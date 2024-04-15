using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NXWalks.API.Controllers
{
    //https:localhost:portnumber/api/Controller
    [Route("api/[controller]")]
    [ApiController]
    public class Controller : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] StudentsName = new string[] { "Saif", "Zain", "Ahsan", "Mubashir" };

            return Ok(StudentsName);
        }
    }
}
