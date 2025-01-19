using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectDisbatch.API.Controllers
{
    //https://localhost:portnumber/api/project
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        // GET: https://localhost:portnumber/api/students
        [HttpGet]
        public IActionResult GetAllProjects()
        {
            string[] exampleNames = new string[] { "Project 1", "Project 2" };
            return Ok(exampleNames);
        }

    }
}
  