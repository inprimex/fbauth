using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    public IActionResult Profile()
    {
        return View(User.Claims ?? Enumerable.Empty<Claim>());
    }

    [AllowAnonymous]
    public IActionResult Error(string? message = null)
    {
        return View(model: message ?? "An unknown error occurred");
    }
}
