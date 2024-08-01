using EventsDAL.DataRepository;
using EventsDAL.Models;
using EventsDAL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventsServiceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _acc;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService acc, ILogger<AccountController> logger)
        {
            this._acc = acc;
            _logger = logger;
        }

        [HttpPost]
        [Route("RegisterUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult RegisterUser([FromBody] Register userdata)
        {
            try
            {
                bool isSuccess = _acc.RegisterUser(userdata);
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
                _logger.LogError(exception: ex, $"StatusCode: {StatusCodes.Status500InternalServerError}, Message:Internal server error ");
                return NotFound();
            }
        }

        [HttpPost]
        [Route("AuthenticateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AuthenticateUser([FromBody] Login userdata)
        {
            try
            {
                User AuthenticatedUser = _acc.AuthenticateUser(userdata);
                if (AuthenticatedUser != null)
                {
                    return Ok(AuthenticatedUser);
                }
                else
                {
                    return Forbid();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(exception: ex, $"StatusCode: {StatusCodes.Status500InternalServerError}, Message:Internal server error ");
                return Forbid();
            }
        }

        [HttpGet]
        [Route("GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllUsers()
        {
            try
            {
                List<User> Users = _acc.GetAllUsers().ToList();
                return Ok(Users);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception: ex, $"StatusCode: {StatusCodes.Status500InternalServerError}, Message:Internal server error ");
                return NotFound();
            }
        }

        [HttpPut]
        [Route("SetUserRole/{userId}/{role}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult SetUserRole(Guid userId, Role role)
        {
            try
            {
                bool isSuccess = _acc.SetUserRole(userId, role);
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
                _logger.LogError(exception: ex, $"StatusCode: {StatusCodes.Status500InternalServerError}, Message:Internal server error ");
                return NotFound();
            }
        }

        [HttpGet]
        [Route("UserExist/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UserExist(string email)
        {
            try
            {
                bool isSuccess = _acc.IsUserExist(email);
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
                _logger.LogError(exception: ex, $"StatusCode: {StatusCodes.Status500InternalServerError}, Message:Internal server error ");
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetUserRole/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUserRole(string email)
        {
            try
            {
                Role? UserRole = _acc.GetUserRole(email);
                return Ok(UserRole);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception: ex, $"StatusCode: {StatusCodes.Status500InternalServerError}, Message:Internal server error ");
                return NotFound();
            }
        }
    }
}
