using EventAppUI.ViewModels;
using EventsDAL.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace EventAppUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string baseurl;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IConfiguration _config, ILogger<AuthController> logger)
        {
            this._config = _config;
            baseurl = _config["WebApiBaseUrl"];
            _logger = logger;
        }

        [HttpGet]
        [Route("Login", Name ="Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("LoginPost",Name ="LoginPost")]
        public async Task<IActionResult> Login(Login logindata)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var httpclient = new HttpClient())
                    {


                        var requestContent = new StringContent(JsonConvert.SerializeObject(logindata), Encoding.UTF8, "application/json");
                        using (var response = await httpclient.PostAsync(baseurl + "/Account/AuthenticateUser", requestContent))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var apiresponse = await response.Content.ReadAsStringAsync();
                                var authenticatedUser = JsonConvert.DeserializeObject<User>(apiresponse);
                                var claims = new List<Claim>
                                {
                                    new Claim(ClaimTypes.Email,logindata.Email),
                                    new Claim(ClaimTypes.Name, authenticatedUser.UserName),
                                    new Claim(ClaimTypes.NameIdentifier, authenticatedUser.Id.ToString()),
                                    new Claim(ClaimTypes.Role,authenticatedUser.UserRole.ToString())
                                };
                                ClaimsIdentity UserIdentity = new ClaimsIdentity(claims,"login");
                                ClaimsPrincipal principal = new ClaimsPrincipal(UserIdentity);
                                await HttpContext.SignInAsync(principal,new AuthenticationProperties() { IsPersistent=logindata.RememberMe});
                                //HttpContext.Session.SetString("userName", authenticatedUser.UserName);

                                // HttpContext.Session.SetString("userRole", Enum.GetName<Role>(authenticatedUser.UserRole));
                                if (authenticatedUser.UserRole.Equals(Role.Admin))
                                {
                                    return RedirectToAction("Home", "Admin");
                                }
                                return RedirectToAction("Home", "Home");
                            }
                            else
                            {
                                TempData["UserLoginFailed"] = "Login Failed.Please enter correct credentials";
                                return View(logindata);
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    _logger.LogError(exception: ex, $"StatusCode: {StatusCodes.Status500InternalServerError}, Message:Internal server error ");

                }
            }

            return View(logindata);
        }


        [HttpGet]
        [Route("Register", Name = "Register")]
        public IActionResult Register()
        {
           
            return View();
        }
 
        [HttpPost]
        [Route("RegisterUser", Name = "RegisterUser")]
        public async Task<IActionResult> Register(Register  registration)
        {
            if(ModelState.IsValid )
            {
                if (registration.Password.Equals(registration.ConfirmPassWord))
                {
                    try
                    {
                        using (var httpclient = new HttpClient())
                        {


                            var requestContent = new StringContent(JsonConvert.SerializeObject(registration), Encoding.UTF8, "application/json");
                            using (var response = await httpclient.PostAsync(baseurl + "/Account/RegisterUser", requestContent))
                            {
                                if (response.IsSuccessStatusCode)
                                {
                                    ModelState.Clear();
                                    TempData["Success"] = "Registration Successful!";
                                    return View();

                                }
                                else
                                {
                                    TempData["Fail"] = "Registration Failed.";
                                    return View(registration);
                                }
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        _logger.LogError(exception: ex, $"StatusCode: {StatusCodes.Status500InternalServerError}, Message:Internal server error ");

                    }

                }
                else
                {
                    ModelState.AddModelError("ConfirmPassword", "Password doesn't match");
                }
              

            }
           
            return View(registration);
        }
        [Authorize]
        [Route("Profile", Name ="Profile")]
        public async Task<IActionResult> Profile()
        {
            return View();
        }

        [Authorize]
        [Route("Logout", Name = "Logout")]
        public async  Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Home");
        }

        [Route("/")]
        [Route("Index",Name = "Index")]
        public IActionResult Home()
        {
            return View();
        }

       

    }
}
