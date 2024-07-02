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
        public TopicController(ITopicsCoveredRepo<TopicCovered> _topicrepo) {
            this._topicrepo = _topicrepo;
        }

        [HttpPost]
        [Route("AddTopic")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddTopic([FromBody] TopicCovered topicData)
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

        [HttpGet]
        [Route("GetAllTopics")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllTopics()
        {
            List<TopicCovered> allTopics=_topicrepo.GetAllTopics().ToList();
            if (allTopics.Count > 0)
            {
                return Ok(allTopics);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetTopicById/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type =typeof(Staff))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTopicById(Guid Id)
        {
            TopicCovered topicExisting = _topicrepo.GetTopicById(Id);
            if(!topicExisting.TopicId.Equals(Guid.Empty))
            {
                return Ok(topicExisting);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        [Route("GetTopicByStaffId/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Staff))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTopicByStaffId(Guid Id)
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

        [HttpGet]
        [Route("GetTopicOnLocationByStaff/{locationId}/{staffId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTopicOnLocationByStaff(Guid locationId, Guid staffId)
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

        [HttpGet]
        [Route("GetTopicForEventLocationByStaff/{eventId}/{locationId}/{staffId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTopicForEventLocationByStaff(Guid eventId,Guid locationId, Guid staffId)
        {
            List<TopicCovered> topics = _topicrepo.GetTopicsByStaffonLocationForEvent(staffId, locationId,eventId).ToList();
            if (topics.Count > 0)
            {
                return Ok(topics);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPut]
        [Route("UpdateTopic")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateTopic(TopicCovered topic)
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


        [HttpDelete]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [Route("DeleteTopic/{id}")]
        public IActionResult DeleteTopic(Guid id)
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


    }
}
