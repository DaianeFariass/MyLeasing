using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Commom.Data;
using System.Text.Json.Serialization;
using System.Text.Json;
using System;

namespace MyLeasing.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly IOwnerRepository _ownerRepository;

        public OwnersController(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }
        [HttpGet]
        public IActionResult GetOwners() 
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                // outras opções de serialização, se necessário
            };

            var jsonString = JsonSerializer.Serialize(_ownerRepository.GetAllWithUsers(), options);
            return Ok(jsonString);
        
        }
       

    }
}
