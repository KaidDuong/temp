using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Rikkonbi.Infrastructure.Identity
{
    public static class IdentityDataInitializer
    {
        public static void SeedData(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                var role = new ApplicationRole
                {
                    Name = "Admin",
                    Description = "Perform all the operations.",
                    CreatedOn = DateTime.Now,
                    CreatedBy = "Initializer"
                };
                roleManager.CreateAsync(role).Wait();
            }

            if (!roleManager.RoleExistsAsync("User").Result)
            {
                var role = new ApplicationRole
                {
                    Name = "User",
                    Description = "Perform normal operations.",
                    CreatedOn = DateTime.Now,
                    CreatedBy = "Initializer"
                };
                roleManager.CreateAsync(role).Wait();
            }

            if (!roleManager.RoleExistsAsync("Sales").Result)
            {
                var role = new ApplicationRole
                {
                    Name = "Sales",
                    Description = "Perform sales operations.",
                    CreatedOn = DateTime.Now,
                    CreatedBy = "Initializer"
                };
                roleManager.CreateAsync(role).Wait();
            }
        }

        private static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByEmailAsync("cooleen.cl@gmail.com").Result == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "kaid",
                    Email = "cooleen.cl@gmail.com",
                    FullName = "Quang",
                    Avatar = "https://lh4.googleusercontent.com/-chSj1z8XLzg/AAAAAAAAAAI/AAAAAAAAAAc/hiAwd5G2L2c/s96-c/photo.jpg",
                    CreatedOn = DateTime.Now,
                    CreatedBy = "Initializer"
                };

                IdentityResult result = userManager.CreateAsync(user, "Default@password123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}