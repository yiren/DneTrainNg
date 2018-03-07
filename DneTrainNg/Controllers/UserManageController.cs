using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DneTrainNg.Models;
using DneTrainNg.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DneTrainNg.Controllers
{
    [Produces("application/json")]
    [Route("api/UserManage")]
    public class UserManageController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public UserManageController(
            ApplicationDbContext db,
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager
            )
        {
            this.db = db;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        // GET: api/UserManage
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/UserManage/5
        [HttpGet("{id}")]
        public string Get(int id)
        {

            return "value";
        }
        
        // POST: api/UserManage
        [HttpPost]
        public async Task<IActionResult> PostForChangePassowrd([FromBody]UserChangePasswordViewModel vm)
        {
            var user = await userManager.FindByNameAsync("dnetrainadmin");

            var changePassword = await userManager.ChangePasswordAsync(user, vm.OldPassword, vm.Password);
            if (!changePassword.Succeeded)
            {
                return BadRequest("原密碼錯誤");
            }
            else
            {
                return Ok("密碼更改成功");
            }
        }
        
        // PUT: api/UserManage/5
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
