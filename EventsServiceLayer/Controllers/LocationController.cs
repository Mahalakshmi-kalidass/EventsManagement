using EventsDAL.DataRepository;
using EventsDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Tracing;

namespace EventsServiceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ICRUDDataRepo<Location> _locRepo;
        public LocationController(ICRUDDataRepo<Location> locRepo) {
            _locRepo = locRepo;
        }

        [HttpPost]
        [Route("AddLocation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddLocation([FromBody] Location locData)
        {
            Location isSuccess = _locRepo.Add(locData);
            if (isSuccess.LocationId!=Guid.Empty)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetAllLocation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllLocation()
        {
            List<Location> allLoc=_locRepo.GetAll().ToList();
            if (allLoc.Count > 0)
            {
                return Ok(allLoc);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetLocationById/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type =typeof(Location))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetLocationById(Guid Id)
        {
            Location locExisting = _locRepo.GetById(Id);
            if(!locExisting.LocationId.Equals(Guid.Empty))
            {
                return Ok(locExisting);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPut]
        [Route("UpdateLocation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateLocation(Location locData)
        {
            bool isSuccess = _locRepo.Update(locData);
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
        [Route("DeleteLocation/{id}")]
        public IActionResult DeleteLocation(Guid id)
        {
            bool isSuccess = _locRepo.Delete(id);
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
