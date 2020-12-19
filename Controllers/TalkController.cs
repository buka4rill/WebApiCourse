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
                var talks = await _repository.GetTalksByMonikerAsync(moniker, true); // We are getting talks by moniker

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
                var talk = await _repository.GetTalkByMonikerAsync(moniker, id, true);

                // Check if talk is present
                if (talk == null) return NotFound("Talk not Found!");

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
                var talk = _mapper.Map<Talk>(model); // map to the talk from the model
                talk.Camp = camp; // set object to camp

                // Account for speaker of Talk
                if (model.Speaker == null) return BadRequest("Speaker ID is required"); // Don't add talk if Speaker is unknown
                var speaker = await _repository.GetSpeakerAsync(model.Speaker.SpeakerId); // Getting speaker from the talk model
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

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TalkModel>> Put(string moniker, int id, TalkModel model)
        {
            // This includes id for the individual talk

            try
            {
                // Get Talk
                var talk = await _repository.GetTalkByMonikerAsync(moniker, id, true);
                if (talk == null) return NotFound("Couldn't find the talk");

                // Map changes from TalkModel into Talk
                _mapper.Map(model, talk);

                // It would map anything from the model into the talk
                // This includes the camp and the speaker
                // To fix, check profile

                // If the model sends the speaker, change it
                if (model.Speaker != null)
                {
                    var speaker = await _repository.GetSpeakerAsync(model.Speaker.SpeakerId);
                    if (speaker != null)
                    {
                        // Set talk speaker obj
                        talk.Speaker = speaker;
                    }
                }

                // Save changes
                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<TalkModel>(talk);
                }
                else
                {
                    return BadRequest("Failed to update database!");
                }
            }
            catch (Exception e)
            {
                // Database failure
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure, \n {e.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(string moniker, int id)
        {
            try
            {

                var talk = await _repository.GetTalkByMonikerAsync(moniker, id);

                if (talk == null) return NotFound("Faild to find the talk to delete");

                // Delete talk
                _repository.Delete(talk); // works because talk is a db type

                // Save changes
                if (await _repository.SaveChangesAsync())
                {
                    return Ok("Talk Deleted!");
                }
                else
                {
                    return BadRequest("Failed to delete talk");
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}