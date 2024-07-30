using EventsDAL.DataRepository;
using EventsDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventsServiceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly IAccessService<EventAccess> _acc;
        private readonly ILogger<AccessController> _logger;

        public AccessController(IAccessService<EventAccess> acc, ILogger<AccessController> logger)
        {
            _acc = acc;
            _logger = logger;
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Create([FromBody] EventAccess access)
        {
            try
            {
                EventAccess success = _acc.Create(access);
                if (success!=null)
                {
                    return Ok(success);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(exception: ex, $"StatusCode: {StatusCodes.Status500InternalServerError}, Message:Internal server error ");
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetEventAccessForUser/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetEventAccessForUser(Guid userId)
        {
            try
            {
                List<EventAccess> success = _acc.GetAccessByUserId(userId).ToList();
                if (success.Count > 0)
                {
                    return Ok(success);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(exception: ex, $"StatusCode: {StatusCodes.Status500InternalServerError}, Message:Internal server error ");
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("delete/{userId}/{eventId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetEventAccessForUser(Guid userId, Guid eventId)
        {
            try
            {
                bool success = _acc.Delete(userId,eventId);
                if (success)
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
                _logger.LogError(exception: ex, $"StatusCode: {StatusCodes.Status500InternalServerError}, Message:Internal server error ");
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetAllAccessInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllAccessInfo()
        {
            try
            {
                List<EventAccess> success = _acc.GetAllAccessInfo().ToList();
                if (success.Count > 0)
                {
                    return Ok(success);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(exception: ex, $"StatusCode: {StatusCodes.Status500InternalServerError}, Message:Internal server error ");
                return NotFound();
            }
        }
    }
}
