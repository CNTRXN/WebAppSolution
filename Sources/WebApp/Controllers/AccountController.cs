using Microsoft.AspNetCore.Mvc;
using WebApp.Models.PageModels;
using ModelLib.DTO;
using WebApp.Settings;
using System.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using EncryptLib;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet("auth")]
        public async Task<IActionResult> AuthAndReg()
        {
            return View(new AuthAndRegResult());
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authorization(string Login, string Password) 
        {
            var tempLogin = await Encrypting.Encrypt(Login);
            var tempPassword = await Encrypting.Encrypt(Password);

            AppSettings.Api.Client.DefaultRequestHeaders.Clear();
            AppSettings.Api.Client.DefaultRequestHeaders.Add("login", Convert.ToBase64String(tempLogin));
            AppSettings.Api.Client.DefaultRequestHeaders.Add("password", Convert.ToBase64String(tempPassword));

            string errorMessage = string.Empty;

            try
            {
                var response = await AppSettings.Api.Client.GetAsync(AppSettings.Api.ApiRequestUrl(ApiRequestType.User, "get"));//"api/User/get");

                if (response.IsSuccessStatusCode)
                {
                    //AppStatics.User = await response.Content.ReadFromJsonAsync<UserDTO>();
                    var receivedUser = await response.Content.ReadFromJsonAsync<UserDTO>();

                    if (receivedUser is UserDTO user)
                    {
                        var claims = new List<Claim>()
                        {
                            new(ClaimTypes.Sid, user.Id.ToString()),
                            new(ClaimTypes.Name, user.Name),
                            new(ClaimTypes.Surname, user.Surname),
                            new("Patronymic", user.Patronymic ?? string.Empty),
                            new(ClaimTypes.DateOfBirth, user.Birthday.ToShortDateString()),
                            new(ClaimTypes.NameIdentifier, Login),
                            new(ClaimTypes.Role, user.Permission.Name ?? "User")
                        };

                        ClaimsIdentity identity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                    }
                }
                else 
                {
                    errorMessage = "Пользователь не найден";
                }
            }
            catch 
            {
                errorMessage = "Ошибка на сервере";
                
            }

            if (!string.IsNullOrEmpty(errorMessage)) 
            {
                TempData["errorMessage"] = errorMessage;

                return View("AuthAndReg");
            }

            return await Task.FromResult<IActionResult>(RedirectToAction("Main", "MainPage"));
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Registration(NewUserDTO userRegData, string rpassword) 
        {
            string errorMessage = string.Empty;
            string succesfulMessage = string.Empty;

            if (userRegData.Password != rpassword)
            {
                errorMessage = "Пароли не совпадают";
            }

            //Если нет ошибок
            if (!string.IsNullOrEmpty(errorMessage)) 
            {
                try
                {
                    NewUserDTO newUser = new() 
                    {
                        Login = userRegData.Login,
                        Name = userRegData.Name,
                        Password = userRegData.Password,
                        Surname = userRegData.Surname,
                        Birthday = userRegData.Birthday,
                        Patronymic = userRegData.Patronymic,
                        PostId = 1,
                    };

                    Console.WriteLine(newUser.Name);

                    var response = await AppSettings.Api.Client.PostAsJsonAsync(AppSettings.Api.ApiRequestUrl(ApiRequestType.User, "registration"), newUser);//"api/User/registration", newUser);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        succesfulMessage = "Вы создали аккаунт!";
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        errorMessage = "Пользователь уже существует";
                    }
                }
                catch 
                {
                    errorMessage = "Ошибка сервера";
                }
            }

            if (!string.IsNullOrEmpty(errorMessage)) 
            {
                TempData["errorMessage"] = errorMessage;

                return View("LogAndReg");
            }
            else if (!string.IsNullOrEmpty(succesfulMessage))
            {
                TempData["succesfulMessage"] = succesfulMessage;
            }

            return View("LogAndReg");
        }

        [HttpGet("profile={id}")]
        public async Task<IActionResult> Profile([FromRoute] int id) 
        {
            var user = new UserDTO();

            try
            {
                var result = await AppSettings.Api.Client.GetFromJsonAsync<UserDTO>(AppSettings.Api.ApiRequestUrl(ApiRequestType.User, $"get/id={id}"));//"api/User/get/id=" + id);

                if (result != null) 
                {
                    user = result;
                }
            }
            catch 
            {
                return BadRequest();
            }

            return View(user);
        }

        [HttpGet("myRequests")]
        public async Task<IActionResult> ShowMyRequests([FromRoute] int profileId) 
        {
            return Ok();
        }
    }
}
