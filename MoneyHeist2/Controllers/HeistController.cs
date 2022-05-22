using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyHeist2.Entities.DTOs.Heist;
using MoneyHeist2.Exceptions;
using MoneyHeist2.Services;

namespace MoneyHeist2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeistController : ControllerBase
    {
        private readonly HeistService _heistService;

        public HeistController(HeistService heistService)
        {            
            _heistService = heistService;
        }

        [Route("createHeist")]
        [HttpPost]
        public IActionResult CreateHeist(HeistRequest request)
        {
            try
            {
                var heist = _heistService.CreateHeist(request);
               
                    return CreatedAtRoute(routeName: "GetHeist",
            routeValues: new { id = heist.ID },
            value: null);
                
            }
            catch (HeistException he)
            {
                //Log he.Message
                return BadRequest(he.UserMessage);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}", Name = "GetHeist")]
        public IActionResult GetMember(Guid id)
        {
            try
            {
                var heist = _heistService.GetHeist(id);

                if (heist == null)
                    return NotFound();

                return Ok(heist);
            }
            catch (HeistException he)
            {

                //Log he.Message
                return BadRequest(he.UserMessage);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }       
    }
}
