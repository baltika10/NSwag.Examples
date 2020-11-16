using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwagWithExamples.Models;
using NSwag.Annotations;

namespace NSwagWithExamples.Controllers
{
    [SwaggerResponse(StatusCodes.Status500InternalServerError, typeof(CustomInternalError))]
    [ApiController, Route("api/v1/cities")]
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, typeof(List<City>))]
        public async Task<IActionResult> GetCities()
        {
            return Ok(new List<City>());
        }

        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, typeof(City))]
        public async Task<IActionResult> GetCity([FromRoute]int id)
        {
            return Ok(new City());
        }
    }
}
