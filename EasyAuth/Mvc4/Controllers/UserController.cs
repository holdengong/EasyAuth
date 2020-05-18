using EasyAuth.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc4.Controllers
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

        [HttpGet("/user/frontchannel-logout")]
        public async Task FrontchannelLogout()
        {
            await HttpContext.SignOutAsync();
            return;
        }

        [HttpPost("/user/backchannel-logout")]
        public async Task BackchannelLogout(Dictionary<string,string> dict)
        {
            await HttpContext.SignOutAsync();
            return;
        }
    }
}
