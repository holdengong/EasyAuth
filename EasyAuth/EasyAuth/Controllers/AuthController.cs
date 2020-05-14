using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EasyAuth.Controllers
{
    public class AuthController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        public AuthController(IIdentityServerInteractionService interaction)
        {
            _interaction = interaction;
        }

        [Route("/")]
        [Route("auth/login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, [FromQuery]string returnUrl)
        {
            var identity = new ClaimsIdentity("idsrv");
            identity.AddClaim(new Claim("sub", model.Username));
            identity.AddClaim(new Claim("username", model.Username));

            var principle = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("idsrv", principle);

            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return Redirect("/Secure");
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var logout = await _interaction.GetLogoutContextAsync(logoutId);
            string  signoutIframeUrl = logout.SignOutIFrameUrl;

            await HttpContext.SignOutAsync();

            return Redirect(signoutIframeUrl);
        }
    }
}
