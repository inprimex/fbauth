using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = FacebookDefaults.AuthenticationScheme;
})
.AddCookie(options => 
{
    options.LoginPath = "/login";
    options.LogoutPath = "/logout";
    options.AccessDeniedPath = "/";
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
})
.AddFacebook(options =>
{
    // Retrieve AppId and AppSecret from configuration with detailed error handling
    var appId = builder.Configuration["Authentication:Facebook:AppId"];
    var appSecret = builder.Configuration["Authentication:Facebook:AppSecret"];

    if (string.IsNullOrWhiteSpace(appId))
    {
        throw new InvalidOperationException(
            "Facebook AppId is not configured. Use 'dotnet user-secrets set \"Authentication:Facebook:AppId\" \"your-app-id\"' to set it."
        );
    }

    if (string.IsNullOrWhiteSpace(appSecret))
    {
        throw new InvalidOperationException(
            "Facebook AppSecret is not configured. Use 'dotnet user-secrets set \"Authentication:Facebook:AppSecret\" \"your-app-secret\"' to set it."
        );
    }

    options.AppId = appId;
    options.AppSecret = appSecret;
    options.Scope.Add("email");
    options.Scope.Add("public_profile");
    options.SaveTokens = true;
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Configure exception handling
builder.Services.AddExceptionHandler(options => 
{
    options.ExceptionHandlingPath = "/Error";
});

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();