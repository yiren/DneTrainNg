﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DneTrainNg.Models;
using DneTrainNg.Models.ViewModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace DneTrainNg.Controllers
{
    [Produces("application/json")]
    [Route("api/Token")]
    [EnableCors("angular")]
    public class TokenController : Controller
    {
        public TokenController(ApplicationDbContext db,
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            dbContext = db;
            RoleManager = roleManager;
            UserManager = userManager;
            SignInManager = signInManager;
            Configuration = configuration;
        }

        protected ApplicationDbContext dbContext { get; private set; }
        protected RoleManager<ApplicationRole> RoleManager { get; private set; }
        protected UserManager<ApplicationUser> UserManager { get; private set; }
        public SignInManager<ApplicationUser> SignInManager { get; private set; }
        protected IConfiguration Configuration { get; private set; }

        // GET: api/Token
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("auth/explictflowAuth/{provider}")]
        public IActionResult FacebookExplicitLogin(string provider, string returnUrl=null)
        {
            switch (provider.ToLower())
            {
                case "facebook":
                    var redirectUrl = Url.Action(nameof(ExternalLoginCallBack), "token", new { returnUrl});
                    var properties = SignInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
                    return Challenge(properties, provider);
                default:
                    return BadRequest(new
                    {
                        Error = String.Format($"Provider:{provider} is not supported.")
                    });
            }
        }

        [HttpGet("ExternalLoginCallBack")]
        public async Task<IActionResult> ExternalLoginCallBack(string returnUrl=null, string remoteError = null)
        {
            if (!String.IsNullOrEmpty(remoteError))
            {
                throw new Exception(String.Format($"External Proivder, ${remoteError}, Has Error"));
            }

            var info = await SignInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                throw new Exception("ERROR: No Login Info");
            }

            var user = await UserManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user == null)
            {
                var emailKey = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
                var email = info.Principal.FindFirst(emailKey).Value;
                user = await UserManager.FindByEmailAsync(email);
                if (user == null)
                {
                    var idKey = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
                    var userName = String.Format($"{info.LoginProvider}{info.Principal.FindFirst(idKey).Value}{Guid.NewGuid().ToString("N")}");
                    user = new ApplicationUser
                    {
                        SecurityStamp = Guid.NewGuid().ToString(),
                        UserName = userName,
                        Email = email
                    };
                    await UserManager.CreateAsync(user, email);

                    await UserManager.AddToRoleAsync(user, "user");
                    
                    user.EmailConfirmed = true;
                    user.LockoutEnabled = false;
                    
                    await dbContext.SaveChangesAsync();

                    var ir = await UserManager.AddLoginAsync(user, info);
                    if (ir.Succeeded)
                    {
                        dbContext.SaveChanges();
                    }
                    else throw new Exception("Authentication Error");

                }
            }
            var rt = CreateRefreshToken("DneTraining", user.Id);
            dbContext.ApplicationTokens.Add(rt);
            dbContext.SaveChanges();
            var roles =await UserManager.GetRolesAsync(user);
            var t = CreateAccessToken(user.Id, roles, rt.Value);
            var res= Content(
                    "<script>window.opener.externalProviderLogin(" +
                    JsonConvert.SerializeObject(t) +
                    ");window.close();" +
                    "</script>", "text/html"
                );
            return res; 
        }

        [HttpPost("auth/facebook")]
        public async Task<IActionResult> FackbookLogin([FromBody]ExternalLoginViewModel model)
        {
            try
            {
                var fbApiUrl = Configuration["ExternalAuth:FB:url"];
                var fbQueryString = String.Format($"me?scope=email&access_token={model.access_token}&fields=id,name,email");

                string result = null;
                using (var c = new HttpClient())
                {
                    c.BaseAddress = new Uri(fbApiUrl);
                    var response = await c.GetAsync(fbQueryString);
                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        throw new Exception("Authentication Error");
                    }
                }

                var epInfo = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                var info = new UserLoginInfo("facebook", epInfo["id"], "Facebook");

                var user = await UserManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

                if (user == null)
                {
                    user = await UserManager.FindByEmailAsync(epInfo["email"]);
                    if (user == null)
                    {
                        var username = String.Format($"FB{epInfo["id"]}{Guid.NewGuid().ToString()}");
                        user = new ApplicationUser
                        {
                            SecurityStamp = Guid.NewGuid().ToString(),
                            Email=epInfo["email"],
                            UserName=username
                        };
                        await UserManager.CreateAsync(user, user.Email);

                        await UserManager.AddToRoleAsync(user, "user");

                        user.EmailConfirmed = true;
                        user.LockoutEnabled = false;
                        dbContext.SaveChanges();
                    }

                    var ir = await UserManager.AddLoginAsync(user, info);
                    
                    if (ir.Succeeded)
                    {
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Authentication Error");
                    }
                }
                var roles = await UserManager.GetRolesAsync(user);

                var rt = CreateRefreshToken(model.client_id, user.Id);
                dbContext.ApplicationTokens.Add(rt);
                dbContext.SaveChanges();

                var t = CreateAccessToken(user.Id, roles, rt.Value);
                return Json(t);
            }
            catch (Exception ex)
            {

                return BadRequest(new { Error = ex.Message });
            }
        }

        private async Task<IActionResult> GetAccessToken(TokenRequestViewModel model)
        {
            try
            {
                var user = await UserManager.FindByNameAsync(model.username);
                if (user == null && model.username.Contains("@"))
                {
                    user = await UserManager.FindByEmailAsync(model.username);
                }

                if (user == null || !await UserManager.CheckPasswordAsync(user, model.password))
                {
                    return new UnauthorizedResult();
                }
                var roles = await UserManager.GetRolesAsync(user);
                //var roleClaims=new List<string>();
               
                var refreshToken = CreateRefreshToken(model.client_id, user.Id);
                dbContext.ApplicationTokens.Add(refreshToken);
                dbContext.SaveChanges();
                var response = CreateAccessToken(user.Id, roles, refreshToken.Value);
                return Json(response);
            }
            catch (Exception e)
            {
                return new UnauthorizedResult();
            }
        }

        private ApplicationToken CreateRefreshToken(string clientId, string userId)
        {
            return new ApplicationToken
            {
                ClientId = clientId,
                UserId = userId,
                Type = 0,
                Value = Guid.NewGuid().ToString("N"),
                CreateDate = DateTime.UtcNow
            };
        }

        private TokenResponseViewModel CreateAccessToken(string userId, IList<string> roles, string refreshToken)
        {
            
            var claims = new List<Claim>
            {
                    new Claim(JwtRegisteredClaimNames.Sub, userId),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString())

            };

            foreach (var role in roles)
            {
                claims.Add(new Claim("roles",role));
            }
            

            var issuerSigninKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Configuration["Auth:Jwt:Key"]));
            var expiration = Configuration.GetValue<int>("Auth:Jwt:TokenExpirationInMinutes");
            var token = new JwtSecurityToken(
                    issuer: Configuration["Auth:Jwt:Issuer"],
                    audience: Configuration["Auth:Jwt:Audience"],
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(expiration)),
                    signingCredentials: new SigningCredentials(issuerSigninKey,
                    SecurityAlgorithms.HmacSha256)
                );
            
            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new TokenResponseViewModel
            {
                expiration = expiration,
                token=encodedToken,
                refresh_token=refreshToken
            };
        }

        private async Task<IActionResult> GetRefreshToken(TokenRequestViewModel model)
        {
            try
            {
                var rt =
                    dbContext.ApplicationTokens
                .FirstOrDefault(t => t.ClientId.Equals(model.client_id) &&
                    t.Value.Equals(model.refresh_token)
                )
                ;

                if (rt == null)
                {
                    return new UnauthorizedResult();
                }

                var user = await UserManager.FindByIdAsync(rt.UserId);
                if (user == null)
                {
                    return new UnauthorizedResult();
                }

                var rtNew = CreateRefreshToken(rt.ClientId, rt.UserId);

                dbContext.ApplicationTokens.Remove(rt);
                dbContext.ApplicationTokens.Add(rtNew);
                dbContext.SaveChanges();
                var roles = await UserManager.GetRolesAsync(user);
                var response = CreateAccessToken(rtNew.UserId, roles, rtNew.ClientId);
                return Json(response);
                //return new UnauthorizedResult(); 
            }
            catch (Exception e)
            {

                return new UnauthorizedResult();
            }
            

            
        }

        [HttpPost("Auth")]
        public async Task<IActionResult> GetToken([FromBody]TokenRequestViewModel model)
        {
            switch (model.grant_type)
            {
                case "password":
                    return await GetAccessToken(model);
                case "refresh_token":
                    return await GetRefreshToken(model);
                default:
                    return new UnauthorizedResult();
            }
        }

        
    }
}
