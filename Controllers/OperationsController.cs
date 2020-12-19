using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebApiPSCourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly IConfiguration _config;

        // Reload configuration
        public OperationsController(IConfiguration config)
        {
            _config = config;
        }

        [HttpOptions("reloadConfig")] // Non resource based verb
        public IActionResult ReloadConfig()
        {
            try
            {
                // Reload, but no method for relod 
                var root = (IConfigurationRoot)_config; // Cast config as IConfigurationRoot interface.
                root.Reload();

                return Ok();

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server Error!");
            }
        }
    }
}