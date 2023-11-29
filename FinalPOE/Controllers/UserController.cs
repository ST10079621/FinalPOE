using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FinalPOE.Models;

namespace FinalPOE.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Action for user registration
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { Username = model.Username };
                var result = await _userManager.CreateAsync(user, model.PasswordHash);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home"); // Redirect to the homepage after registration
                }
            }

            return View(model);
        }

        // Action for user login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.PasswordHash, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home"); // Redirect to the homepage after successful login
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt");
            return View(model);
        }

        // Action for user logout
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home"); // Redirect to the homepage after logout
        }

    }
}
