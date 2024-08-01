using EventAppUI.ViewModels;
using EventsDAL.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
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

        [HttpGet]
        [Route("GoogleLogin",Name ="GoogleLogin")]
        public IActionResult GLogin()
        {
            //challenge initiates the authentication flow with the external provider.
            //generally to authenticate the user with external provider,
            //we need to give authentication property like redirect uri
            //(after authentication , redirected to mentioned redirect uri) and authentication scheme eg. google's / facebook's
            return Challenge(new AuthenticationProperties { RedirectUri = "/" }, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet]
        [Route("GoogleResponse", Name = "GoogleResponse")]
        public async Task<IActionResult> GoogleResponse()
        {
           
                var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
                if (result.Succeeded)
                {
                    var claims = result.Principal.Claims.ToList();
                    var userEmail = result.Principal.FindFirstValue(ClaimTypes.Email);
                    var userName = result.Principal.FindFirstValue(ClaimTypes.Name);

                    try
                    {
                        using (var httpClient = new HttpClient())
                        {
                            // Check if user exists
                            var response = await httpClient.GetAsync($"{baseurl}/Account/UserExist/{userEmail}");
                            if (response.IsSuccessStatusCode)
                            {
                                // Get user role
                                var roleResponse = await httpClient.GetAsync($"{baseurl}/Account/UserRole/{userEmail}");
                                if (roleResponse.IsSuccessStatusCode)
                                {
                                    var userRole = await roleResponse.Content.ReadAsStringAsync();
                                    claims.Add(new Claim(ClaimTypes.Role, userRole));
                                }
                            }
                            else
                            {
                                // Register user
                                var registerModel = new Register
                                {
                                    Email = userEmail,
                                    FirstName = userName.Split(" ")[0],
                                    LastName = userName.Split(" ")[1],
                                };
                                var requestContent = new StringContent(JsonConvert.SerializeObject(registerModel), Encoding.UTF8, "application/json");
                                var registerResponse = await httpClient.PostAsync($"{baseurl}/Account/RegisterUser", requestContent);
                                if (registerResponse.IsSuccessStatusCode)
                                {
                                    TempData["Success"] = "Registration Successful!";
                                }
                                else
                                {
                                    TempData["Fail"] = "Registration Failed.";
                                }
                            }
                        }

                        // Sign in user
                        var userIdentity = new ClaimsIdentity(claims);
                        var principal = new ClaimsPrincipal(userIdentity);
                    //ViewBag.principal = principal;
                    //ViewBag.claims = claims;
                    HttpContext.Session.SetString("role", principal.FindFirstValue(ClaimTypes.Role));
                        await HttpContext.SignInAsync(principal, new AuthenticationProperties { IsPersistent = true });

                        return RedirectToAction("Home", "Home");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(exception: ex, $"StatusCode: {StatusCodes.Status500InternalServerError}, Message:Internal server error ");
                    }
                }
               
                    TempData["UserLoginFailed"] = "Login Failed. Please enter correct credentials";
                    return View("Login");
                
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
           // await HttpContext.SignOutAsync(GoogleDefaults.AuthenticationScheme);
            return RedirectToAction("Home");
        }

        [Route("/")]
        [Route("Index",Name = "Index")]
        public IActionResult Home()
        {
            ViewData["UserInfo"] = HttpContext.User.Claims;
            return View();
        }

       

    }
}
