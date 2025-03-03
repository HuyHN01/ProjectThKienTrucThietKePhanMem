using ASC.Model.BaseTypes;
using ASC.Web.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ASC.Web.Data
{
    public class IdentitySeed : IIdentitySeed
    {
        public async Task Seed(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<ApplicationSettings> options)
        {
            // roles là List<string>, không cần sử dụng Split
            var roles = options.Value.Roles;

            foreach (var role in roles)
            {
                try
                {
                    if (!roleManager.RoleExistsAsync(role).Result)
                    {
                        IdentityRole storageRole = new IdentityRole
                        {
                            Name = role
                        };
                        IdentityResult roleResult = await roleManager.CreateAsync(storageRole);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            var adminUser = await userManager.FindByEmailAsync(options.Value.AdminEmail);
            if (adminUser == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = options.Value.AdminName,
                    Email = options.Value.AdminEmail,
                    EmailConfirmed = true
                };

                IdentityResult result = await userManager.CreateAsync(user, options.Value.AdminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
                }
                else
                {
                    Console.WriteLine($"Error creating admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }

            var engineerUser = await userManager.FindByEmailAsync(options.Value.EngineerEmail);
            if (engineerUser == null)
            {
                IdentityUser engineer = new IdentityUser
                {
                    UserName = options.Value.EngineerName,
                    Email = options.Value.EngineerEmail,
                    EmailConfirmed = true
                };

                IdentityResult result = await userManager.CreateAsync(engineer, options.Value.EngineerPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(engineer, Roles.Engineer.ToString());
                }
                else
                {
                    Console.WriteLine($"Error creating engineer user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}
