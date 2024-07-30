using EventsDAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Text;

namespace EventAppUI.Controllers
{
    [Authorize]
    [Route("Location")]
    public class LocationController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string baseurl;
        public LocationController(IConfiguration _config) 
        { 
            this._config = _config;
            baseurl = _config["WebApiBaseUrl"];
        }

       

        
        [Route("EventLocation/{eventId}",Name = "EventLocation")]
        public async Task<IActionResult> EventLocation(Guid eventId)
        {
            List<Location> eventLocation = await LoadEventLocations(eventId);
            ViewBag.EventId = eventId;//to be used in view for deletion operation 
            return View(eventLocation);
        }

        [Route("StaffsInEvent/{eventId}/{locId}")]
        public async Task<IActionResult> EventStaffs(Guid eventId, Guid locId)
        {

            List<Staff> staffs = await LoadStaff(eventId, locId);
            staffs = staffs.DistinctBy(s=>s.StaffId).ToList();
            return View(staffs);
        }

        [Route("TopicsInEvent/{eventId}/{locId}/{staffId}")]
        public async Task<IActionResult> TopicsInEvent(Guid eventId, Guid locId,Guid staffId)
        {

            List<TopicCovered> topics = await LoadTopics(eventId, locId, staffId);
            return View(topics);
        }

        [Authorize(Roles = "Owner,EventManager")]
        [Route("AddLocation/{eventId}", Name = "AddLocation")]
        public async Task<IActionResult> AddLocation(Guid eventId)
        {
            ViewBag.eventId = eventId;
           
            await getAllLocation();
            await getAllStaffs();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Owner,EventManager")]
        [Route("AddAllocation", Name = "AddAllocation")]
        public async Task<IActionResult> AddAllocation(EventAllocation eventAllocation)
        {

            using (var httpclient = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(eventAllocation), Encoding.UTF8, "application/json");
                using (var response = await httpclient.PostAsync(baseurl + "/eventAllocation/AddEventAllocation", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                       
                        return RedirectToAction("EventLocation", new {eventId = eventAllocation.EventId});
                    }


                }
            }
            await getAllLocation();
            await getAllStaffs();
            return RedirectToAction("AddLocation", new {eventId = eventAllocation.EventId});
        }

        [Authorize(Roles = "Owner,EventManager")]
        [Route("DeleteEventAllocation/{id}/{locId}", Name = "DeleteEventAllocation")]
        public async Task<IActionResult> DeleteEventAllocation(Guid id, Guid locId)
        {
            using (var httpclient = new HttpClient())
            {
                using(var response = await httpclient.DeleteAsync(baseurl+ $"/eventallocation/DeleteEventAllocationByEventId/{id}/{locId}"))
                {
                    if (response.IsSuccessStatusCode)
                    {

                        return RedirectToAction("EventLocation", new { eventId = id });
                    }
                }
            }
            return RedirectToAction("EventLocation", new { eventId = id });

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
        public async Task<List<TopicCovered>> GetEventStaffTopicId(Guid eventId, Guid locationId, Guid staffId)
        {
            List<TopicCovered> topics = new List<TopicCovered>();
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync(baseurl + $"/topic/GetTopicForEventLocationByStaff/{eventId}/{locationId}/{staffId}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiresponse = await response.Content.ReadAsStringAsync();
                        topics = JsonConvert.DeserializeObject<List<TopicCovered>>(apiresponse);

                    }
                }
            }
            return topics;
        }

        [NonAction]
        public async Task<TopicCovered> getTopicDetail(Guid topicId)
        {
            TopicCovered topic = new TopicCovered();
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync(baseurl + $"/topic/GetTopicById/{topicId}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiresponse = await response.Content.ReadAsStringAsync();
                        topic = JsonConvert.DeserializeObject<TopicCovered>(apiresponse);

                    }
                }
            }
            return topic;
        }

        [NonAction]
        public async Task<List<TopicCovered>> LoadTopics(Guid eventId, Guid locationId,Guid staffId)
        {
            List<TopicCovered> topics = await GetEventStaffTopicId(eventId, locationId,staffId);
            //List<TopicCovered> topics = new List<TopicCovered>();
            //foreach (var item in topicIds)
            //{
            //    var detail = await getStaffDetail(item.TopicId);
            //    topics.Add(item);
            //}
            return topics;
        }


        [NonAction]
        public async Task<List<EventAllocation>> GetEventstaffId(Guid eventId, Guid locationId)
        {
            List<EventAllocation> eventStaff = new List<EventAllocation>();
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync(baseurl + $"/eventallocation/GetEventStaffByEventLocation/{eventId}/{locationId}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiresponse = await response.Content.ReadAsStringAsync();
                       eventStaff= JsonConvert.DeserializeObject<List<EventAllocation>>(apiresponse);

                    }
                }
            }
            return eventStaff;
        }

        [NonAction]
        public async Task<Staff> getStaffDetail(Guid staffId)
        {
            Staff staff = new Staff();
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync(baseurl + $"/staff/GetStaffById/{staffId}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiresponse = await response.Content.ReadAsStringAsync();
                        staff = JsonConvert.DeserializeObject<Staff>(apiresponse);

                    }
                }
            }
            return staff;
        }
        [NonAction]
        public async Task<List<Staff>> LoadStaff(Guid eventId, Guid locationId)
        {
           List<EventAllocation> staffIds =  await GetEventstaffId(eventId, locationId);
            List<Staff> staffs = new List<Staff>();
            foreach(var staff in staffIds)
            {
                var detail = await getStaffDetail(staff.StaffId);
                staffs.Add(detail);
            }
            return staffs;
        }

        [NonAction]
        public async Task<List<Location>> LoadEventLocations(Guid eventId)
        {
            List<Location> locations= new List<Location>();
            using(var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{baseurl}/eventallocation/GetEventsByEventId/{eventId}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiresponse = await response.Content.ReadAsStringAsync ();
                        List<EventAllocation> eventdetails = JsonConvert.DeserializeObject<List<EventAllocation>>(apiresponse);
                        var locationsId = eventdetails.DistinctBy(e => e.LocationId).ToList();
                      
                        foreach (var loc in locationsId) {
                           var location =  await getLocation(loc.LocationId);
                            locations.Add(location);
                        }


                    }
                }
            }
            return locations;
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
    }
}
