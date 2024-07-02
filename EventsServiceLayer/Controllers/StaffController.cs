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
        public StaffController(IStaffDataRepo<Staff> _staffrepo) {
            this._staffrepo = _staffrepo;
        }

        [HttpPost]
        [Route("AddStaff")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddStaff([FromBody] Staff staffData)
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

        [HttpGet]
        [Route("GetAllStaff")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllStaff()
        {
            List<Staff> allStaff=_staffrepo.GetAllStaff().ToList();
            if (allStaff.Count > 0)
            {
                return Ok(allStaff);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetStaffById/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type =typeof(Staff))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetStaffById(Guid Id)
        {
            Staff staffExisting = _staffrepo.GetStaffById(Id);
            if(!staffExisting.StaffId.Equals(Guid.Empty))
            {
                return Ok(staffExisting);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        [Route("GetStaffByLocationId/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Staff))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetStaffByLocationId(Guid LocationId)
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

        [HttpPut]
        [Route("UpdateStaff")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateStaff(Staff staff)
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


        [HttpDelete]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [Route("DeleteStaff/{id}")]
        public IActionResult DeleteStaff(Guid id)
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


    }
}
