using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyLeasing.Web.Data;
using System.Text.Json.Serialization;
using System.Text.Json;

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
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                // outras opções de serialização, se necessário
            };

            var jsonString = JsonSerializer.Serialize(_lesseeRepository.GetAllWithUser(), options);
            return Ok(jsonString);

        }

    }
}
