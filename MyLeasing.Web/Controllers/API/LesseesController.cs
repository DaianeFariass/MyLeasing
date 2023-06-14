using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyLeasing.Web.Data;

namespace MyLeasing.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class LesseesController : ControllerBase
    {
        private readonly ILesseeRepository _lesseeRepository;

        public LesseesController(ILesseeRepository lesseeRepository)
        {
            _lesseeRepository = lesseeRepository;
        }
        [HttpGet]
        public IActionResult GetLesses()
        {
            return Ok(_lesseeRepository.GetAllWithUser());

        }

    }
}
