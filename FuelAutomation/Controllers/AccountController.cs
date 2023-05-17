using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using FuelAutomation.Entity;
using FuelAutomation.Models;
using FuelAutomation.Identity;
using Microsoft.AspNetCore.Authorization;

namespace FuelAutomation.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {

        private UserManager<Users> _userManager;
        private SignInManager<Users> _signInManager;

        public AccountController(UserManager<Users>  userManager,SignInManager<Users> signInManager)
        {
            _signInManager= signInManager;
            _userManager= userManager;
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel )
        {

            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }
        //    loginModel.Email = loginModel.Email.ToUpper();
            var user= await _userManager.FindByEmailAsync(loginModel.Email);

            if (user == null)
            {

                ModelState.AddModelError("Email", "Geçersiz mail adresi");
                return View(loginModel);
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, true, false);

            if (!result.Succeeded) {

                ModelState.AddModelError("Password", "Geçersiz şifre");

            }
            var roles= await _userManager.GetRolesAsync
                (user);
            if (result.Succeeded)
            {
                if (roles.Contains("Admin"))
                {
                    return RedirectToAction("Dashboard","Admin");


                }
                if(roles.Contains("Staff"))
                {
                    return RedirectToAction("Index","Home");
                }

            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {

            return View();
        }
    }
}
