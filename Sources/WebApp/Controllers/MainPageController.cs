﻿using Microsoft.AspNetCore.Mvc;
using WebApp.Models.DTO;
using WebApp.Models.EquipData;
using WebApp.Models.UserData;
using WebApp.Settings;

namespace WebApp.Controllers
{
    public class MainPageController : Controller
    {
        [HttpGet]
        public IActionResult Main() 
        {
            if (HttpContext.Request.Cookies.ContainsKey("state-id")) 
            {
                var stateId = HttpContext.Request.Cookies["state-id"];

                Console.WriteLine(stateId);
            }

            if (AppStatics.User is UserDTO user) 
            {
                return View(user);
            }

            return View();
        }
    }
}
