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
        private readonly ILogger<LocationController> _logger;

        public LocationController(ICRUDDataRepo<Location> locRepo, ILogger<LocationController> logger)
        {
            _locRepo = locRepo;
            _logger = logger;
        }

        [HttpPost]
        [Route("AddLocation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddLocation([FromBody] Location locData)
        {
            try
            {


                Location isSuccess = _locRepo.Add(locData);
                if (isSuccess.LocationId != Guid.Empty)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetAllLocation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllLocation()
        {
            try
            {


                List<Location> allLoc = _locRepo.GetAll().ToList();
                if (allLoc.Count > 0)
                {
                    return Ok(allLoc);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetLocationById/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type =typeof(Location))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetLocationById(Guid Id)
        {
            try
            {


                Location locExisting = _locRepo.GetById(Id);
                if (!locExisting.LocationId.Equals(Guid.Empty))
                {
                    return Ok(locExisting);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }

        }

        [HttpPut]
        [Route("UpdateLocation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateLocation(Location locData)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
        }


        [HttpDelete]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [Route("DeleteLocation/{id}")]
        public IActionResult DeleteLocation(Guid id)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
        }


    }
}
