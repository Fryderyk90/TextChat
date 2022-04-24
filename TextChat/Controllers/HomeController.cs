using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using TextChat.Models;
using TextChat.ViewModels;

namespace TextChat.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public HomeController(ILogger<HomeController> logger, SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LogInViewModel input)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(
                input.Username,
                input.Password,
                false,
                false
            );

            if (result.Succeeded)
            {
                return RedirectToAction("ChatRoom");
            }
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        var clearFields = new LogInViewModel();
        return PartialView("_login", clearFields);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel input)
    {
        if (ModelState.IsValid)
        {
            var user = new IdentityUser {UserName = input.Username};
            var result = await _userManager.CreateAsync(user, input.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return View("~/Views/Chat/ChatRoom.cshtml");
            }
        }

        return PartialView("_register", input);
    }

    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    public async Task<IActionResult> ChatRoom()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var chatRoomViewModel = new ChatRoomViewModel() {Username = currentUser.UserName};
        return View("~/Views/Chat/ChatRoom.cshtml", chatRoomViewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}