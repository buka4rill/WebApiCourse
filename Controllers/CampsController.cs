// using Microsoft.AspNetCore.Components;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WebApiPSCourse.Data;
using System.Threading.Tasks;
using WebApiPSCourse.Models;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Linq;
using WebApiPSCourse.Data.Entities;
using Microsoft.AspNetCore.Routing;

namespace WebApiPSCourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampsController : ControllerBase
    {
        private readonly ICampRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CampRepository> _logger;
        private readonly LinkGenerator _linkGenerator;

        public CampsController(ICampRepository repository, IMapper mapper, ILogger<CampRepository> logger, LinkGenerator linkGenerator)
        {
            _mapper = mapper;
            _logger = logger;
            _linkGenerator = linkGenerator;
            _repository = repository;
        }


        // Get All Camps Array
        [HttpGet]
        public async Task<ActionResult<CampModel[]>> Get(bool includeTalks = false)
        {
            try
            {
                var results = await _repository.GetAllCampsAsync(includeTalks);

                // CampModel[] models = _mapper.Map<CampModel[]>(results); // Create or map a CampModel Array from results

                // return Ok(results);
                // return Ok(models);

                // Since ActionResult returns a type that matches models, it returns Ok for us
                // return models;
                return _mapper.Map<CampModel[]>(results);
            }
            catch (Exception e)
            {
                // return status 500 for database failure
                // ModelState.AddModelError("", e.Message);
                _logger.LogInformation(e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure, \n {e.Message}");
            }

            // return Ok(new { Moniker = "LAG2018", Name = "Made In Lagos Camp" });

        }

        // Get a single Camp
        [HttpGet("{moniker}")]
        public async Task<ActionResult<CampModel>> Get(string moniker)
        {
            try
            {
                var result = await _repository.GetCampAsync(moniker);

                // If result is null return 404
                if (result == null) return NotFound("Camp not Found");

                return _mapper.Map<CampModel>(result);

            }
            catch (Exception)
            {
                // Database error
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        // Search by Date
        [HttpGet("search")]
        public async Task<ActionResult<CampModel[]>> SearchByDate(DateTime date, bool includeTalks = false)
        {
            try
            {
                var results = await _repository.GetAllCampsByEventDate(date, includeTalks);

                // If we don''t find any results
                if (!results.Any()) return NotFound("No results for this search!");

                return _mapper.Map<CampModel[]>(results);

            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure, \n {e.Message}");
            }
        }


        // Create a new Camp(POST)
        public async Task<ActionResult<CampModel>> Post(CampModel model)
        {
            try
            {
                // Model Validation
                // if (string.IsNullOrWhiteSpace(model.Name))

                //  Validate Moniker
                var existingCamp = await _repository.GetCampAsync(model.Moniker); // Get camp by moniker
                if (existingCamp != null)
                {
                    return BadRequest("Moniker exists already!");
                }

                // We use link generator to make link uri dynamic
                var location = _linkGenerator.GetPathByAction("Get", "Camps", new { moniker = model.Moniker });

                // Check that location is valid
                if (string.IsNullOrWhiteSpace(location))
                {
                    return BadRequest("Could not use current moniker");
                }



                var camp = _mapper.Map<Camp>(model); //Map a Camp object from the CampModel from request

                // Add camp to repository
                _repository.Add(camp);

                // if changes worked
                if (await _repository.SaveChangesAsync())
                {
                    // Created
                    // Include location string (URI) and
                    // map new camp back to the campmodel 
                    return Created($"api/camps/{camp.Moniker}", _mapper.Map<CampModel>(camp)); // Not ideal if we decide to change the link later
                }
            }
            catch (Exception e)
            {
                // Log Error
                _logger.LogInformation(e.Message);

                // Database Failure Error 500
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure, \n {e.Message}");
            }

            // Else return Bad Request - Changes didn't save
            return BadRequest("POST failed! Changes did not save!");
        }


        // Updating the Camp (PUT)
        [HttpPut("{moniker}")]
        public async Task<ActionResult<CampModel>> Put(string moniker, CampModel model)
        {
            try
            {
                // Old Camp object
                var oldCamp = await _repository.GetCampAsync(moniker);

                // If not found...return error 404
                if (oldCamp == null) return NotFound($"Could not find camp with moniker of {moniker}");

                // oldCamp.Name = model.Name; // Not interesting

                // Using mapper, update
                _mapper.Map(model, oldCamp); // Taking the data from the model and applying to oldCamp

                if (await _repository.SaveChangesAsync())
                {
                    // Ok, map back to the Camp model
                    return _mapper.Map<CampModel>(oldCamp);
                }
            }
            catch (Exception e)
            {
                // Database failure
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure, \n {e.Message}");
            }

            return BadRequest("PUT request failed!");
        }

        [HttpDelete("{moniker}")]
        public async Task<IActionResult> Delete(string moniker)
        {
            // IAction result because we are only returning a statuscode.
            // There is no body once you delete an item 
            // There is no body to map to a Get

            try
            {
                // Repository Camp
                var oldCamp = await _repository.GetCampAsync(moniker);

                // Check if it exists, else return 404
                if (oldCamp == null) return NotFound($"Could not find camp with moniker of {moniker}");

                // Camp gotten? Delete it
                _repository.Delete(oldCamp);

                // Save changes
                if (await _repository.SaveChangesAsync())
                {
                    return Ok($"Camp with moniker of {moniker} deleted!");
                }
            }
            catch (Exception e)
            {
                // Database failure
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure, \n {e.Message}");
            }

            // Return Bad request incase db doesn't change
            return BadRequest("Failed to delete the camp!");
        }
    }
}