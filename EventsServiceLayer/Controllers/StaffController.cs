using EventsDAL.DataRepository;
using EventsDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Tracing;

namespace EventsServiceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffDataRepo<Staff> _staffrepo;
        private readonly ILogger<StaffController>  _logger;

        public StaffController(IStaffDataRepo<Staff> _staffrepo, ILogger<StaffController> logger)
        {
            this._staffrepo = _staffrepo;
            _logger = logger;
        }

        [HttpPost]
        [Route("AddStaff")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddStaff([FromBody] Staff staffData)
        {
            try
            {


                bool isSuccess = _staffrepo.AddStaff(staffData);
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

        [HttpGet]
        [Route("GetAllStaff")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllStaff()
        {
            try
            {
                List<Staff> allStaff = _staffrepo.GetAllStaff().ToList();
                if (allStaff.Count > 0)
                {
                    return Ok(allStaff);
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
        [Route("GetStaffById/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type =typeof(Staff))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetStaffById(Guid Id)
        {
            try
            {
                Staff staffExisting = _staffrepo.GetStaffById(Id);
                if (!staffExisting.StaffId.Equals(Guid.Empty))
                {
                    return Ok(staffExisting);
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
        [Route("GetStaffByLocationId/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Staff))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetStaffByLocationId(Guid LocationId)
        {
            try
            {
                List<Staff> staffExisting = _staffrepo.GetStaffsByLocationId(LocationId).ToList();
                if (staffExisting.Count > 0)
                {
                    return Ok(staffExisting);
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
        [Route("UpdateStaff")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateStaff(Staff staff)
        {
            try
            {
                bool isSuccess = _staffrepo.UpdateStaff(staff);
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
        [Route("DeleteStaff/{id}")]
        public IActionResult DeleteStaff(Guid id)
        {
            try
            {
                bool isSuccess = _staffrepo.DeleteStaff(id);
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
