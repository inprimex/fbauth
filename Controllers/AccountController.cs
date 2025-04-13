using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IConfiguration _configuration;

    public AccountController(ILogger<AccountController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [HttpGet("login")]
    public IActionResult Login(string? returnUrl = null)
    {
        // Store the return URL in the properties so we can redirect after login
        var redirectUrl = Url.Action("FacebookResponse", "Account", new { returnUrl }) ?? "/";
        var properties = new AuthenticationProperties 
        { 
            RedirectUri = redirectUrl 
        };
        
        return Challenge(properties, FacebookDefaults.AuthenticationScheme);
    }

    [HttpGet("facebook-response")]
    public async Task<IActionResult> FacebookResponse(string? returnUrl = null)
    {
        try
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            if (!authenticateResult.Succeeded)
            {
                _logger.LogWarning("Authentication failed");
                return RedirectToAction("Error", "Home", new { message = "Authentication failed" });
            }

            var claims = authenticateResult.Principal?.Identities.FirstOrDefault()?.Claims 
                ?? Enumerable.Empty<Claim>();
            var facebookId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            _logger.LogInformation("User {Name} with ID {FacebookId} successfully authenticated", name, facebookId);

            // Optional: Log or process user information
            // You can add user registration logic here

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during Facebook authentication");
            return RedirectToAction("Error", "Home", new { message = "An error occurred during authentication" });
        }
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}