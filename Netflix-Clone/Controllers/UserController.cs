using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Netflix_Clone.Data;
using Netflix_Clone.Models;

namespace Netflix_Clone.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        private readonly ApplicationDbContext _dbContext;

        public UserController(SignInManager<User> signInManager, UserManager<User> userManager, ApplicationDbContext dbContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [Authorize]
        public IActionResult Index()
        {
            if(!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model, string returnUrl = null)
        {
            User? user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (user == null) return NotFound();

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName!, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "User");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            User user = new User() { UserName = model.Username, Email = model.Email };
            var result = await _userManager.CreateAsync(user,  model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "User");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return RedirectToAction("Register", "Home");


        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
