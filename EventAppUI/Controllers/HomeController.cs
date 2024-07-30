using EventsDAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace EventAppUI.Controllers
{
    [Authorize]
    [Route("Home",Name ="Home")]
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string baseurl;
     
        public HomeController(IConfiguration _config) 
        { 
            this._config = _config;
            baseurl = _config["WebApiBaseUrl"];
           

        }

       

        [HttpGet]
      //  [Route("/")]
        [Route("Home",Name = "Home")]
        public async Task<IActionResult> Home()
        {
            List<Event> allEvents = new List<Event>();
            using(var httpclient = new HttpClient())
            {
                using(var response = await httpclient.GetAsync(baseurl+"/Event/GetAllEvent"))
                {
                    if(response.IsSuccessStatusCode)
                    {
                        var apiresponse = await response.Content.ReadAsStringAsync();
                        allEvents = JsonConvert.DeserializeObject<List<Event>>(apiresponse);

                    }
                }
            }
            ViewBag.Username = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            ViewData["UserInfo"] = HttpContext.User;
            return View(allEvents);
        }
        [Authorize(Roles = "Owner")]
        [HttpGet]
        [Route("NewEvent",Name ="NewEvent")]
        public async Task<IActionResult> NewEvent()
        {
            await getAllLocation();
            await getAllStaffs();
            return View();
        }

        [HttpGet]
        [Route("EditEvent/{Id}",Name = "EditEvent")]
        [Authorize(Roles = "Owner,EventManager")]
        public async Task<IActionResult> EditEvent(Guid Id)
        {
            Event data = await LoadEvent(Id);
           return View(data);
        }

        [HttpPost]
        [Authorize(Roles = "Owner,EventManager")]
        [Route("UpdateEvent",Name ="UpdateEvent")]
        public async Task<IActionResult> UpdateEvent(Event newEvent)
        {
            using(var httpclient = new HttpClient())
            {

                StringContent content = new StringContent( JsonConvert.SerializeObject(newEvent),Encoding.UTF8,"application/json");
                using (var response = await httpclient.PutAsync(baseurl + "/event/UpdateEvent", content))
                {
                    if( response.IsSuccessStatusCode)
                    {
                        ViewBag.eventData = response.Content.ReadAsStringAsync();
                        return RedirectToAction("Home");
                    }
                   
                    
                }
            }
            return View("EditEvent",new {Id = newEvent.EventId});
        }
        [Authorize(Roles="Owner,EventManager")]
        [Route("DeleteEvent/{Id}",Name ="DeleteEvent")]
        public async Task<IActionResult> DeleteEvent(Guid Id)
        {
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.DeleteAsync($"{baseurl}/Event/DeleteEvent/{Id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Home");
                    }
                    
                }
            }
            return RedirectToAction("Home");
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

                        ViewData["Events"] =eventdetails;

                    }
                }
            }
        }

        //[Route("EventLocation/{eventId}",Name = "EventLocation")]
        //public async Task<IActionResult> EventLocation(Guid eventId)
        //{
        //    await LoadEventLocations(eventId);
        //    return View();
        //}

        [NonAction]
        public async Task LoadEventLocations(Guid eventId)
        {
            using(var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{baseurl}/eventallocation/GetEventsByEventId/{eventId}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiresponse = await response.Content.ReadAsStringAsync ();
                        List<EventAllocation> eventdetails = JsonConvert.DeserializeObject<List<EventAllocation>>(apiresponse);
                        var locationsId = eventdetails.DistinctBy(e => e.LocationId).ToList();
                       List<Location> locations = new List<Location>();
                        foreach (var loc in locationsId) {
                           var location =  await getLocation(loc.LocationId);
                            locations.Add(location);
                        }


                    }
                }
            }
        }
        [NonAction]
        public async Task<Event> LoadEvent(Guid eventId)
        {
            Event eventdetails = new Event();
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{baseurl}/event/GetEventById/{eventId}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiresponse = await response.Content.ReadAsStringAsync();
                        eventdetails = JsonConvert.DeserializeObject<Event>(apiresponse);
                      


                    }
                }
            }
            return eventdetails;
        }

        [NonAction]
        public async Task<Location> getLocation(Guid locationId)
        {
            Location location = new Location();
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{baseurl}/location/GetLocationById/{locationId}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiresponse = await response.Content.ReadAsStringAsync();
                        location = JsonConvert.DeserializeObject<Location>(apiresponse);
                       

                    }
                }
            }
            return location;
        }

        [NonAction]
        public async Task getAllLocation()
        {
            List<Location> location = new List<Location>();
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{baseurl}/location/GetAllLocation"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiresponse = await response.Content.ReadAsStringAsync();
                        location = JsonConvert.DeserializeObject<List<Location>>(apiresponse);
                        ViewBag.Locations = location;

                    }
                }
            }
            
        }

        
      

        [NonAction]
        
        public async Task getAllStaffs()
        {
            List<Staff> staffs = new List<Staff>();
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{baseurl}/Staff/GetAllStaff"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiresponse = await response.Content.ReadAsStringAsync();
                        staffs = JsonConvert.DeserializeObject<List<Staff>>(apiresponse);
                        ViewBag.Staffs = staffs;

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
