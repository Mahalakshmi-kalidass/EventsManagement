using EventsDAL.DataRepository;
using EventsDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Tracing;

namespace EventsServiceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ICRUDDataRepo<Event> _eventRepo;
        public EventController(ICRUDDataRepo<Event> eventRepo) {
            _eventRepo = eventRepo;
        }

        [HttpPost]
        [Route("AddEvent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddEvent([FromBody] Event eventData)
        {
            Event isSuccess = _eventRepo.Add(eventData);
            if (isSuccess.EventId!=Guid.Empty)
            {
                return Ok(isSuccess);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetAllEvent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllEvents()
       {
            List<Event> allEvents = _eventRepo.GetAll().ToList();
            if (allEvents.Count > 0)
            {
                return Ok(allEvents);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetEventById/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type =typeof(Event))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetEventById(Guid Id)
        {
            Event eventExisting = _eventRepo.GetById(Id);
            if(!eventExisting.EventId.Equals(Guid.Empty))
            {
                return Ok(eventExisting);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPut]
        [Route("UpdateEvent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateEvent(Event eventData)
        {
            bool isSuccess =_eventRepo.Update(eventData);
            if (isSuccess)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [Route("DeleteEvent/{id}")]
        public IActionResult DeleteEvent(Guid id)
        {
            bool isSuccess = _eventRepo.Delete(id);
            if (isSuccess)
            {
                return Ok();
            }
            else
            {
                  return NotFound();
            }

        }




    }
}
