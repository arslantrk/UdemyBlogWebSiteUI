using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyBlogWebSiteUI.Models;

namespace UdemyBlogWebSiteUI.Controlllers
{
    public class AccountController : Controller
    {
        public IActionResult SingIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SingIn(AppUserLoginModel appUserLoginModel)
        {
            return View();
        }
    }
}
