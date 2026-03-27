using Microsoft.AspNetCore.Identity;

namespace MiniEcommerMVC.Data
{
    public class DbInitializer
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            // this rolemanager is used to create and manage roles
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            // this userManager is used to crete and manage user
            var userManager = service.GetRequiredService<UserManager<IdentityUser>>();

            // define role here 
            string[] roles = { "Admin", "Customer" };

            foreach (var role in roles)
            {
                // check if role exists in DB or not
                if(!await roleManager.RoleExistsAsync(role))
                {
                    // if not then create a role
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // admin credentials define 
            var adminEmail = "admin@gmail.com";
            var adminPassword = "Admin@123";

            // check if admin exists or not
            var user = await userManager.FindByEmailAsync(adminEmail);


            
            // if admin not exist then create one
            if(user == null)
            {
                var newUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail
                };

                // create User in DB and it also save user in DB and hash password in DB
                await userManager.CreateAsync(newUser, adminPassword);

                // then here we are assigning role("admin") to the user that we created
                await userManager.AddToRoleAsync(newUser, "Admin");
            }



        }
    }
}
