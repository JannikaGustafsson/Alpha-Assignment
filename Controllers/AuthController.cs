using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using WebApp.Entities;
using WebApp.Models;

namespace WebApp.Controllers;

public class AuthController : Controller
{
    private RegisterViewModel _registerViewModel = new();

    public UserManager<ApplicationUser> UserManager { get; }
    public SignInManager<ApplicationUser> SignInManager { get; }


    public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        UserManager = userManager;
        SignInManager = signInManager;
    }

    /*Login*/
    [HttpGet]
    public IActionResult Login(string returnUrl = "~/")
    {
        ViewBag.ReturnUrl = returnUrl;
        return View(new LogInModel());
    }

    [HttpPost]
    public async Task<IActionResult> Login(LogInModel formData, string returnUrl = "/Home/Index")
    {
        ViewBag.ReturnUrl = returnUrl;

        if (!ModelState.IsValid)
            return View(formData);

        var user = await UserManager.FindByEmailAsync(formData.Email);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(formData);
        }

        var result = await SignInManager.PasswordSignInAsync(user, formData.Password, false, false);
        if (result.Succeeded)
        {
            return LocalRedirect(returnUrl);
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View(formData);
    }


    /*Register*/

    [HttpGet]
    public IActionResult Register()
    {
        return View(_registerViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserModel formData)
    {
        if (!ModelState.IsValid)
        {
            _registerViewModel.FormData = formData;
            TempData["Error"] = "ModelState was invalid.";
            return View(_registerViewModel);
        }

        var newUser = new ApplicationUser
        {
            Email = formData.Email,
            UserName = formData.Email,
            FirstName = formData.FirstName,
            LastName = formData.LastName
        };

        var result = await UserManager.CreateAsync(newUser, formData.Password);

        if (result.Succeeded)
        {
            TempData["RegisterSuccess"] = "Account created successfully!";
            return RedirectToAction("Login", "Auth");
        }
        else
        {
            TempData["Error"] = string.Join(", ", result.Errors.Select(e => e.Description));
        }

        _registerViewModel.FormData = formData;
        return View(_registerViewModel);
    }

    /*Logout*/
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Login", "Auth");
    }
}
