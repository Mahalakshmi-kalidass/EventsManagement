using EventsDAL.DataRepository;
using EventsDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Tracing;

namespace EventsServiceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventAllocationController : ControllerBase
    {
        private readonly IEventAllocationDataRepo<EventAllocation> _eventalloc;
        public EventAllocationController(IEventAllocationDataRepo<EventAllocation> _eventalloc) {
            this._eventalloc = _eventalloc;
        }

        [HttpPost]
        [Route("AddEventAllocation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddEventAllocation([FromBody] EventAllocation eveAlloc)
        {
            bool isSuccess = _eventalloc.AddEventAllocation(eveAlloc);
            if (isSuccess)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetAllEventAllocation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllEventAllocation()
        {
            List<EventAllocation> allEvents=_eventalloc.GetAllEventsAllocation().ToList();
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
        [Route("GetEventAllocationById/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type =typeof(EventAllocation))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetEventAllocationById(Guid Id)
        {
            EventAllocation eventExisting = _eventalloc.GetEventAllocationById(Id);
            if(!eventExisting.EventAllocationId.Equals(Guid.Empty))
            {
                return Ok(eventExisting);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        [Route("GetEventsByLocationId/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Staff))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetEventsByLocationId(Guid LocationId)
        {
            List<EventAllocation> eventsExisting = _eventalloc.GetEventAllocatedByLocationId(LocationId).ToList();
            if (eventsExisting.Count > 0)
            {
                return Ok(eventsExisting);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        [Route("GetEventsByEventId/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetEventsByEventId(Guid Id)
        {
            List<EventAllocation> eventsExisting = _eventalloc.GetAllocationsByEventId(Id).ToList();
            if (eventsExisting.Count > 0)
            {
                return Ok(eventsExisting);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpGet]
        [Route("GetEventStaffByEventLocation/{eventId}/{locationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetEventStaffByEventLocation(Guid eventId,Guid locationId)
        {
            List<EventAllocation> eventsExisting = _eventalloc.GetStaffAllocationByEvent(eventId,locationId).ToList();
            if (eventsExisting.Count > 0)
            {
                return Ok(eventsExisting);
            }
            else
            {
                return NotFound();
            }

        }


        [HttpPut]
        [Route("UpdateEventAllocation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateEventAllocation(EventAllocation eventData)
        {
            bool isSuccess = _eventalloc.UpdateEventAllocation(eventData);
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
        [Route("DeleteEventAllocation/{id}")]
        public IActionResult DeleteEventAllocation(Guid id)
        {
            bool isSuccess = _eventalloc.DeleteEventAllocation(id);
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
        [Route("DeleteEventAllocationByEventId/{id}/{locId}")]
        public IActionResult DeleteEventAllocationByEventId(Guid id, Guid locId)
        {
            bool isSuccess = _eventalloc.DeleteEventAllocationByEventId(id,locId);
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
