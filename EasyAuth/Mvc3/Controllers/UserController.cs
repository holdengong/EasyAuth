using EasyAuth.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc1.Controllers
{
    public class UserController : Controller
    {
        [HttpGet("/user/logout")]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync();
            await HttpContext.SignOutAsync("oidc");
            return;
        }

        [HttpGet("/user/logout-notify")]
        public async Task LogoutNotify()
        {
            await HttpContext.SignOutAsync();
            return;
        }
    }
}
