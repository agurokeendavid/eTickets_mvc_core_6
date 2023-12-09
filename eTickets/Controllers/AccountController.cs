using eTickets.Data;
using eTickets.Data.Static;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace eTickets.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly AppDbContext _context;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
        AppDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    [HttpGet]
    public IActionResult Login() => View(new LoginViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var user = await _userManager.FindByEmailAsync(viewModel.EmailAddress);

        if (user != null)
        {
            var passwordCheck = await _userManager.CheckPasswordAsync(user, viewModel.Password);
            if (passwordCheck)
            {
                var result = await _signInManager.PasswordSignInAsync(user, viewModel.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Movies");
                }
            }
            TempData["Error"] = "Wrong credentials. Please, try again!";
        }
        else
        {
            TempData["Error"] = "Wrong credentials. Please, try again!";
        }

        
        return View(viewModel);
    }
    
    [HttpGet]
    public IActionResult Register() => View(new RegisterViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);
        
        var user = await _userManager.FindByEmailAsync(viewModel.EmailAddress);
        if (user != null)
        {
            TempData["Error"] = "This email address is already in use.";
            return View(viewModel);
        }

        var newUser = new ApplicationUser()
        {
            FullName = viewModel.FullName,
            Email = viewModel.EmailAddress,
            UserName = viewModel.EmailAddress
        };
        var newUserResponse = await _userManager.CreateAsync(newUser, viewModel.Password);

        if (newUserResponse.Succeeded)
        {
            await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            return View("RegisterCompleted");
        }

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Movies");
    }
}