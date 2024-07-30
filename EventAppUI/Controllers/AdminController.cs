using EventsDAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EventAppUI.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string baseurl;
        private readonly ILogger<AdminController> _logger;
        public AdminController(IConfiguration _config, ILogger<AdminController> logger)
        {
            this._config = _config;
            baseurl = _config["WebApiBaseUrl"];
            _logger = logger;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("Home",Name ="AdminHome")]
        public async Task<IActionResult> Home()
        {
            await GetAllUsers();
             await LoadAllEvents();
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("Access", Name = "Access")]
        public async Task<IActionResult> AccessManagement()
        {
            await LoadAllEvents();
            await GetAllUsers();

            await GetAllAccessInfo();
            return View();
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        [Route("RoleManagement",Name ="Role")]
        public async Task<IActionResult> RoleManagement()
        {
            await GetAllUsers();
            return View();
        }

        [Authorize(Roles ="Admin")]
        [Route("Profile", Name = "AdminProfile")]
        public  IActionResult Profile()
        {
            return View();
        }




        [NonAction]
        public async Task LoadAllEvents()
        {
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{baseurl}/event/GetAllEvent"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiresponse = await response.Content.ReadAsStringAsync();
                        var eventdetails = JsonConvert.DeserializeObject<List<Event>>(apiresponse);

                        ViewData["Events"] = eventdetails;

                    }
                }
            }
        }
        [NonAction]
        public async Task GetAllUsers()
        {
            List<User> Users = new List<User>();
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{baseurl}/Account/GetAllUsers"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiresponse = await response.Content.ReadAsStringAsync();
                        Users = JsonConvert.DeserializeObject<List<User>>(apiresponse);
                        if(Users.Count > 0)
                        {
                            Users = Users.Where(u => u.UserRole != Role.Admin || u.UserRole==null).ToList();

                        }
                        ViewData["Users"] = Users;

                    }
                }
            }
        }



        [NonAction]
        public async Task GetAllAccessInfo()
        {
            List<EventAccess> EventAccessInfo = new List<EventAccess>();
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{baseurl}/access/GetAllAccessInfo"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiresponse = await response.Content.ReadAsStringAsync();
                        EventAccessInfo = JsonConvert.DeserializeObject<List<EventAccess>>(apiresponse);
                        ViewData["AccessInfo"] = EventAccessInfo;

                    }
                }
            }
        }
    }
}
