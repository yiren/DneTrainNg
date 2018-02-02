using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DneTrainNg.Models;
using DneTrainNg.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DneTrainNg.Controllers
{
    [Produces("application/json")]
    [Route("api/Token")]
    
    public class TokenController : Controller
    {
        public TokenController(ApplicationDbContext db,
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            dbContext = db;
            RoleManager = roleManager;
            UserManager = userManager;
            Configuration = configuration;
        }

        protected ApplicationDbContext dbContext { get; private set; }
        protected RoleManager<ApplicationRole> RoleManager { get; private set; }
        protected UserManager<ApplicationUser> UserManager { get; private set; }
        protected IConfiguration Configuration { get; private set; }

        // GET: api/Token
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public async Task<IActionResult> FackbookLogin([FromBody]ExternalLoginViewModel model)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
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
                Claim[] roleClaims;

                StringBuilder sb = new StringBuilder();
                foreach (var role in roles)
                {
                    //roleClaims.Add(new Claim(JwtRegisteredClaimNames.ro role));
                }

                var refreshToken = CreateRefreshToken(model.client_id, user.Id);
                dbContext.ApplicationTokens.Add(refreshToken);
                dbContext.SaveChanges();
                var response = CreateAccessToken(user.Id, refreshToken.Value);
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

        private TokenResponseViewModel CreateAccessToken(string userId, string refreshToken)
        {
            
            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, userId),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString())

                };

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

                var user = UserManager.FindByIdAsync(rt.UserId);
                if (user == null)
                {
                    return new UnauthorizedResult();
                }

                var rtNew = CreateRefreshToken(rt.ClientId, rt.UserId);

                dbContext.ApplicationTokens.Remove(rt);
                dbContext.ApplicationTokens.Add(rtNew);
                dbContext.SaveChanges();
                var response = CreateAccessToken(rtNew.UserId, rtNew.ClientId);
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
            switch (model.grand_type)
            {
                case "password":
                    return await GetAccessToken(model);
                case "refresh_token":
                    return await GetRefreshToken(model);
                default:
                    return new UnauthorizedResult();
            }
        }


        // GET: api/Token/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Token
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Token/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
