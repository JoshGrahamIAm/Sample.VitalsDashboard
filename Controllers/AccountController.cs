using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

public class AccountController : Controller
{
  private readonly ICustomPasswordHasher _passwordHasher;
  private readonly ApplicationDbContext _dbContext;

  public AccountController(ICustomPasswordHasher passwordHasher, ApplicationDbContext dbContext)
  {
    //inject dbcontext and pasword hasher
    _passwordHasher = passwordHasher;
    _dbContext = dbContext;
  }

  // GET: /Account/Login
  [AllowAnonymous]
  public IActionResult Login()
  {
    return View();
  }

  // POST: /Account/Login
  [HttpPost]
  [AllowAnonymous]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Login(LoginViewModel model)
  {
    if (ModelState.IsValid)
    {
      // Check if the user exists in the database
      var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.UserName == model.Username);
      if (user != null && _passwordHasher.VerifyPassword(model.Password, user.PasswordHash))
      {
        // Create the user claims
        var claims = new[]
        {
          new Claim(ClaimTypes.Name, user.UserName),
        };

        // Create and sign in the user using the claims
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return RedirectToAction("Index", "Dashboard");
      }

      ModelState.AddModelError(string.Empty, "Invalid login attempt.");
    }

    return View(model);
  }

  // GET: /Account/Register
  [AllowAnonymous]
  public IActionResult Register()
  {
    return View();
  }

  // POST: /Account/Register
  [HttpPost]
  [AllowAnonymous]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Register(RegisterViewModel model)
  {
    if (ModelState.IsValid)
    {
      // Check if the user already exists
      if (await _dbContext.Users.AnyAsync(u => u.UserName == model.Username))
      {
        ModelState.AddModelError("Username", "Username is already taken.");
        return View(model);
      }

      // Hash the password before storing it in the database
      var hashedPassword = _passwordHasher.HashPassword(model.Password);

      // Create the new user
      var user = new ApplicationUser
      {
        UserName = model.Username,
        PasswordHash = hashedPassword,

      };

      // Add the user to the database
      _dbContext.Users.Add(user);
      await _dbContext.SaveChangesAsync();

      // Redirect to the login page after successful registration
      return RedirectToAction("Login");
    }

    return View(model);
  }

  // POST: /Account/Logout
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Logout()
  {
    // Sign out the user
    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

    return RedirectToAction("Index", "Home");
  }
}
