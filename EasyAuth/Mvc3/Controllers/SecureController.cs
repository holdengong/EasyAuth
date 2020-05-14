using EasyAuth.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc1.Controllers
{
    public class SecureController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            var claims = HttpContext.User.Claims;

            var userVm = new UserViewModel
            {
                Name = claims.FirstOrDefault(_ => _.Type == "username")?.Value
            };

            return View(userVm);
        }
    }
}
