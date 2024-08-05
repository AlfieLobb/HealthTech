using Identity.Api.Models;

namespace Identity.Api;

public class UsersSeed(ILogger<UsersSeed> logger, UserManager<ApplicationUser> userManager) : IDbSeeder<ApplicationDbContext>
{
    public async Task SeedAsync(ApplicationDbContext context)
    {
        var admin = await userManager.FindByNameAsync("admin");

        if (admin == null)
        {
            admin = new ApplicationUser
            {
                Id = "3983e8a3-a0c8-4d38-917f-feed4ce28fa2",
                UserName = "admin",
                Email = "admin@6bHealthTech.co.uk",
                EmailConfirmed = true,
                CardHolderName = "admin",
                CardNumber = "4012888888881881",
                CardType = 1,
                City = "Lancaster",
                Country = "UK",
                Expiration = "12/24",
                LastName = "admin",
                Name = "admin",
                PhoneNumber = "07530612121",
                ZipCode = "LA1 5LZ",
                State = "Lancashire",
                Street = "37 Gerrard Street",
                SecurityNumber = "123"
            };

            var result = userManager.CreateAsync(admin, "Pa55w0rd!").Result;

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("admin created");
            }
        }
        else
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("admin already exists");
            }
        }
    }
}
