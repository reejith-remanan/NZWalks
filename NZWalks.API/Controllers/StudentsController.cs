using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    // https://localhost:7029/students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        //GET Method
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studnetNames = new string[] { "Reejith", "Megha", "Rahul", "Remya" };

            return Ok(studnetNames); // OK response -> success in ASP.Net
        }
    }
}
