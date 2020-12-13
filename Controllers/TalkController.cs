using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebApiPSCourse.Data;
using WebApiPSCourse.Data.Entities;
using WebApiPSCourse.Models;

namespace WebApiPSCourse.Controllers
{

    [Route("api/camps/{moniker}/talks")] // This is an associaton controller
    [ApiController]
    public class TalkController : ControllerBase
    {
        private readonly ICampRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        // Controller
        public TalkController(ICampRepository repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        // Get List of Talks related to a Camp
        [HttpGet]
        public async Task<ActionResult<TalkModel[]>> Get(string moniker)
        {
            try
            {
                var talks = await _repository.GetTalksByMonikerAsync(moniker); // We are getting talks by moniker

                return _mapper.Map<TalkModel[]>(talks);
            }
            catch (Exception e)
            {
                // Database failure
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure, \n {e.Message}");
            }
        }

        // Get individual Talk
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TalkModel>> Get(string moniker, int id)
        {
            try
            {
                // We are getting talks by moniker and id fpr individual talk
                var talk = await _repository.GetTalkByMonikerAsync(moniker, id);

                return _mapper.Map<TalkModel>(talk);
            }
            catch (Exception e)
            {
                // Database failure
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure, \n {e.Message}");
            }
        }

        // Create a new talk for a camp
        [HttpPost]
        public async Task<ActionResult<TalkModel>> Post(string moniker, TalkModel model)
        {
            try
            {
                // Add Talk to database
                // Get Camp first
                var camp = await _repository.GetCampAsync(moniker); // validate that moniker belongs to a camp
                if (camp == null) return BadRequest("Camp does not exist!");

                // Camp exists...map to Talk
                var talk = _mapper.Map<Talk>(camp);
                talk.Camp = camp; // set object to camp

                // Account for speaker of Talk
                if (model.Speaker == null) return BadRequest("Speaker ID is required"); // Don't add talk if Speaker is unknown
                var speaker = await _repository.GetSpeakerAsync(model.Speaker.SpeakerId);
                if (speaker == null) return BadRequest("Speaker could not be found"); // Bad informationg given
                // If OK
                talk.Speaker = speaker;

                _repository.Add(talk);

                if (await _repository.SaveChangesAsync())
                {
                    // URl binding to the individual talk to send a location
                    var url = _linkGenerator.GetPathByAction(HttpContext, "Get", values: new { moniker, id = talk.TalkId });

                    return Created(url, _mapper.Map<TalkModel>(talk));
                }
                else
                {
                    return BadRequest("Failed to save new Talk");
                }
            }
            catch (Exception e)
            {
                // Database failure
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure, \n {e.Message}");
            }
        }
    }
}