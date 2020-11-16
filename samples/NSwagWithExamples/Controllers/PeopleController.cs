using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NSwagWithExamples.Models;
using NSwag.Annotations;

namespace NSwagWithExamples.Controllers
{
    [ApiController, Route("api/v1/people")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, typeof(CustomInternalError))]
    public class PeopleController : ControllerBase
    {
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, typeof(List<Person>), Description = "This response contains a list of all the persons")]
        public async Task<IActionResult> GetPeople()
        {
            return Ok(new List<Person>());
        }

        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, typeof(Person))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, typeof(CustomInternalErrorOnMethodLevel))]
        public async Task<IActionResult> GetPerson([FromRoute]int id)
        {
            return Ok(new Person());
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody, BindRequired] Person person)
        {
            // create person logic
            return Ok();
        }
    }
}