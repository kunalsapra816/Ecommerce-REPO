using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniEcommerMVC.Models.ViewModels;



namespace MiniEcommerMVC.Controllers
{
    public class AccountController: Controller
    {

        // Dependency Injection

        // 1 SignInManager -> Handles Authentication(login, logout , cookie sessions)
        private readonly SignInManager<IdentityUser> _signInManager;

        //2. UserManager -> Handle user related operations(Create User, add role, Find User)
        private readonly UserManager<IdentityUser> _userManager;


        // Constructor defination (here we are doing dependency Injection)
        public AccountController
            (
                SignInManager<IdentityUser> signInManager,
                UserManager<IdentityUser> userManager)
                
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        //Login GET

        // this GET method will show login page if user didn't logined and after loggined it redirect back to original URL.
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        //Login POST
        //
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model , string returnUrl = null)
        {

            // validation
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            //In this step lot of things happens
            // 1. Check DB(AspNetUsers)
            // 2. Verify Password hash
            // 3. Create auth cookie.
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            // if login success redirect to home page or original page
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains("Admin"))
                    return Redirect("/Admin/Products");

                return Redirect("/");
            }


            // other show failure in login 
            ModelState.AddModelError("", "Invalid login");
            return View(model);
        }

        //Register GET
        //Just to load register form
        public IActionResult Register()
        {
            return View();
        }


        //Register POST
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {

            // validation
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            // create User object
            var user = new IdentityUser
            {
                UserName = model.Email,
                Email  = model.Email
            };

            // this hash password and save user in AspNetUsers
            var result = await _userManager.CreateAsync(user, model.Password);


            // if success then redirect to login page and assign role as well
            if(result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer");
                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        //Logout
        //Removes authentication cookie and logout user
        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Logout()
{
    await _signInManager.SignOutAsync();
    return RedirectToAction("Login");
}

    }
}
