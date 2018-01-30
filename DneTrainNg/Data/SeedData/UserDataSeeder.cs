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

            var user_admin = new ApplicationUser
            {
                UserName = "Admin",
                Email = "admin@nedtpc.com.tw",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = false
            };

            if (await userManager.FindByNameAsync(user_admin.UserName) == null)
            {
                await userManager.CreateAsync(user_admin, "admin");
                await userManager.AddToRoleAsync(user_admin, admin_role);
                await userManager.AddToRoleAsync(user_admin, general_user);
            }

            var user_tony = new ApplicationUser
            {
                UserName = "Tony",
                Email = "tony@nedtpc.com.tw",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = false
            };

            if (await userManager.FindByNameAsync(user_tony.UserName) == null)
            {
                await userManager.CreateAsync(user_tony, "tony");
                await userManager.AddToRoleAsync(user_admin, general_user);
            }

            var user_john = new ApplicationUser
            {
                UserName = "John",
                Email = "john@nedtpc.com.tw",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = false
            };

            if (await userManager.FindByNameAsync(user_john.UserName) == null)
            {
                await userManager.CreateAsync(user_john, "john");
                await userManager.AddToRoleAsync(user_admin, general_user);
            }

            var user_amy = new ApplicationUser
            {
                UserName = "Amy",
                Email = "amy@nedtpc.com.tw",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = false
            };

            if (await userManager.FindByNameAsync(user_amy.UserName) == null)
            {
                await userManager.CreateAsync(user_amy, "amy");
                await userManager.AddToRoleAsync(user_admin, general_user);
            }

            await db.SaveChangesAsync();
        }


    }
}

