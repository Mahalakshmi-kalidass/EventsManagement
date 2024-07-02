using EventsDAL.DataRepository;
using EventsDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Tracing;

namespace EventsServiceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ITopicsCoveredRepo<TopicCovered> _topicrepo;
        private readonly ILogger<TopicController> _logger;

        public TopicController(ITopicsCoveredRepo<TopicCovered> _topicrepo, ILogger<TopicController> logger)
        {
            this._topicrepo = _topicrepo;
            _logger = logger;
        }

        [HttpPost]
        [Route("AddTopic")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddTopic([FromBody] TopicCovered topicData)
        {
            try
            {
                bool isSuccess = _topicrepo.AddTopic(topicData);
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
        [Route("GetAllTopics")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllTopics()
        {
            try
            {


                List<TopicCovered> allTopics = _topicrepo.GetAllTopics().ToList();
                if (allTopics.Count > 0)
                {
                    return Ok(allTopics);
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
        [Route("GetTopicById/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type =typeof(Staff))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTopicById(Guid Id)
        {
            try
            {
                TopicCovered topicExisting = _topicrepo.GetTopicById(Id);
                if (!topicExisting.TopicId.Equals(Guid.Empty))
                {
                    return Ok(topicExisting);
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
        [Route("GetTopicByStaffId/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Staff))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTopicByStaffId(Guid Id)
        {
            try
            {
                List<TopicCovered> topics = _topicrepo.GetTopicsByStaff(Id).ToList();
                if (topics.Count > 0)
                {
                    return Ok(topics);
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
        [Route("GetTopicOnLocationByStaff/{locationId}/{staffId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTopicOnLocationByStaff(Guid locationId, Guid staffId)
        {
            try
            {
                List<TopicCovered> topics = _topicrepo.GetTopicsByStaffsOnLocation(staffId, locationId).ToList();
                if (topics.Count > 0)
                {
                    return Ok(topics);
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
        [Route("GetTopicForEventLocationByStaff/{eventId}/{locationId}/{staffId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTopicForEventLocationByStaff(Guid eventId,Guid locationId, Guid staffId)
        {
            try
            {
                List<TopicCovered> topics = _topicrepo.GetTopicsByStaffonLocationForEvent(staffId, locationId, eventId).ToList();
                if (topics.Count > 0)
                {
                    return Ok(topics);
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
        [Route("UpdateTopic")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateTopic(TopicCovered topic)
        {
            try
            {
                bool isSuccess = _topicrepo.UpdateTopic(topic);
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
        [Route("DeleteTopic/{id}")]
        public IActionResult DeleteTopic(Guid id)
        {
            try
            {
                bool isSuccess = _topicrepo.DeleteTopic(id);
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
