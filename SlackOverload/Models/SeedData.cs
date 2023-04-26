using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SlackOverload.Areas.Identity.Data;
using SlackOverload.Data;

namespace SlackOverload.Models
{
    public static class SeedData
    {
        public async static Task Initialize(IServiceProvider serviceProvider)
        {
            SlackContext context = new SlackContext(serviceProvider.GetRequiredService<DbContextOptions<SlackContext>>());

            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (!context.Roles.Any())
            {
                List<string> roles = new List<string> { "Administrator", "User" };

                foreach (string r in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole(r));
                }

                await context.SaveChangesAsync();
            }

            if (!context.Users.Any())
            {
                ApplicationUser defaultAdministrator = new ApplicationUser
                {
                    DisplayName = "Admin",
                    Email = "admin34@gmail.com",
                    NormalizedEmail = "ADMIN34@GMAIL.COM",
                    UserName = "admin34@gmail.com",
                    NormalizedUserName = "ADMIN34@GMAIL.COM",
                    EmailConfirmed = true
                };

                ApplicationUser FirstUser = new ApplicationUser
                {
                    DisplayName = "Alexa",
                    Email = "alexa25@gmail.com",
                    NormalizedEmail = "ALEXA25@GMAIL.COM",
                    UserName = "alexa25@gmail.com",
                    NormalizedUserName = "ALEXA25@GMAIL.COM",
                    EmailConfirmed = false
                };

                ApplicationUser SecondUser = new ApplicationUser
                {
                    DisplayName = "Georgie",
                    Email = "george80@yahoo.com",
                    NormalizedEmail = "GEORGIE80@YAHOO.COM",
                    UserName = "georgie80@yahoo.com",
                    NormalizedUserName = "GEORGIE80@YAHOO.COM",
                    EmailConfirmed = true
                };

                ApplicationUser ThirdUser = new ApplicationUser
                {
                    DisplayName = "Tina",
                    Email = "tina45@gmail.com",
                    NormalizedEmail = "TINA45@GMAIL.COM",
                    UserName = "tina45@gmail.com",
                    NormalizedUserName = "TINA45@GMAIL.COM",
                    EmailConfirmed = true
                };

                PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
                string AdminHashedPassword = passwordHasher.HashPassword(defaultAdministrator, "Admin@34");

                string FirstUserPassword = passwordHasher.HashPassword(FirstUser, "Alexa@25");
                string SecondUserPassword = passwordHasher.HashPassword(SecondUser, "Georgie@80");
                string ThirdUserPassword = passwordHasher.HashPassword(ThirdUser, "Tina@45");

                defaultAdministrator.PasswordHash = AdminHashedPassword;
                FirstUser.PasswordHash = FirstUserPassword;
                SecondUser.PasswordHash = SecondUserPassword;
                ThirdUser.PasswordHash = ThirdUserPassword;

                await userManager.CreateAsync(defaultAdministrator);
                await userManager.AddToRoleAsync(defaultAdministrator, "Administrator");

                await userManager.CreateAsync(FirstUser);
                await userManager.AddToRoleAsync(FirstUser, "User");

                await userManager.CreateAsync(SecondUser);
                await userManager.AddToRoleAsync(SecondUser, "User");

                await userManager.CreateAsync(ThirdUser);
                await userManager.AddToRoleAsync(ThirdUser, "User");

            }

            await context.SaveChangesAsync();
        }
    }
}
