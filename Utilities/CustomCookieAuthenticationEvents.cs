using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
{
  private readonly ILogger<CustomCookieAuthenticationEvents> _logger;

  public CustomCookieAuthenticationEvents(ILogger<CustomCookieAuthenticationEvents> logger)
  {
    _logger = logger;
  }

  public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
  {
    // Custom logic when redirecting to the login page
    _logger.LogInformation("Redirecting to login page.");

    return base.RedirectToLogin(context);
  }

  public override Task RedirectToLogout(RedirectContext<CookieAuthenticationOptions> context)
  {
    // Custom logic when redirecting to the logout page
    _logger.LogInformation("Redirecting to logout page.");

    return base.RedirectToLogout(context);
  }

  public override Task RedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> context)
  {
    // Custom logic when redirecting to the access denied page
    _logger.LogInformation("Redirecting to access denied page.");

    return base.RedirectToAccessDenied(context);
  }

  public override Task RedirectToReturnUrl(RedirectContext<CookieAuthenticationOptions> context)
  {
    // Custom logic when redirecting to the return URL
    _logger.LogInformation("Redirecting to return URL.");

    return base.RedirectToReturnUrl(context);
  }

  public override Task SignedIn(CookieSignedInContext context)
  {
    // Custom logic after the user has been signed in
    _logger.LogInformation("User has been signed in.");

    return base.SignedIn(context);
  }

  public override Task SigningIn(CookieSigningInContext context)
  {
    // Custom logic before the user is signed in
    _logger.LogInformation("User is being signed in.");

    return base.SigningIn(context);
  }

  public override Task SigningOut(CookieSigningOutContext context)
  {
    // Custom logic before the user is signed out
    _logger.LogInformation("User is being signed out.");

    return base.SigningOut(context);
  }

  public override Task ValidatePrincipal(CookieValidatePrincipalContext context)
  {
    // Custom logic to validate the principal/identity of the user
    _logger.LogInformation("Validating user principal.");

    return base.ValidatePrincipal(context);
  }
}
