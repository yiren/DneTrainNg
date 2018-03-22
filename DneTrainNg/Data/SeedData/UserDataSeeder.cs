using DneTrainNg.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DneTrainNg.Data.SeedData
{
    public class UserDataSeeder
    {
        public static void SeedIdentityData(ApplicationDbContext db,
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager
            )
        {
            if (!db.Users.Any())
            {
                CreateUsers(db, roleManager, userManager)
                           .GetAwaiter()
                           .GetResult();
            }
        }

        private static async Task CreateUsers(ApplicationDbContext db, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            string admin_role = "DataAdmin";
            string general_user = "user";

            if (!await roleManager.RoleExistsAsync(admin_role))
            {
                await roleManager.CreateAsync(new ApplicationRole(admin_role));
            }
            if (!await roleManager.RoleExistsAsync(general_user))
            {
                await roleManager.CreateAsync(new ApplicationRole(general_user));
            }

            var train_admin = new ApplicationUser
            {
                UserName = "dnetrainadmin",
                Email = "trainadmin@nedtpc.com.tw",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = false
            };
            var vote_admin = new ApplicationUser
            {
                UserName = "admin",
                Email = "voteadmin@nedtpc.com.tw",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = false
            };



            if (await userManager.FindByNameAsync(train_admin.UserName) == null)
            {
                await userManager.CreateAsync(train_admin, "test");
                await userManager.AddToRoleAsync(train_admin, admin_role);
                await userManager.AddToRoleAsync(train_admin, general_user);
            }

            if (await userManager.FindByNameAsync(vote_admin.UserName) == null)
            {
                await userManager.CreateAsync(vote_admin, "1qaz@WSX");
                await userManager.AddToRoleAsync(vote_admin, admin_role);
                await userManager.AddToRoleAsync(vote_admin, general_user);
            }
            await db.SaveChangesAsync();
        }


    }
}

