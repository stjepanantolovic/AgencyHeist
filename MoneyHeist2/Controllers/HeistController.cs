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

        [HttpPut]
        [Route("{heist_id}/skills")]
        public IActionResult UpdateHeistSkills(Guid heist_id, UpdateHeistSkillRequest request)
        {
            try
            {
                var heist = _heistService.GetHeist(heist_id);

                if (heist == null)
                    return NotFound();
                _heistService.UpdateHeistSkills(heist, request);

                var response = new ContentResult() { StatusCode = 204, };
                var locationUrl = $"{heist_id}/skills";
                Response.Headers.Add("Location", Request.Path.Value);

                return response;
            }
            catch (HeistException he)
            {
                //Log he.Message
                return BadRequest(he.UserMessage);
            }

            catch (Exception ex)
            {
                //LogDefineOptions ex.Message
                return BadRequest("Unexpected error occured");
            }
        }

        [HttpGet]
        [Route("{heist_id}/skills")]
        public IActionResult GetMemberSkills(Guid heist_id)
        {
            var heist = _heistService.GetHeist(heist_id);

            if (heist == null)
            {
                return NotFound();
            }

            var response = new UpdateHeistSkillResponse() { Skills = _heistService.GetHeistSkillResponsFromHeistSkillLevels(heist.HeistSkillLevels.ToList()) };           

            return Ok(response);
        }

        [HttpGet]
        [Route("{heist_id}/eligible_members")]
        public IActionResult GetEligibleMembers(Guid heist_id)
        {
            var heist = _heistService.GetHeist(heist_id);

            if (heist == null)
            {
                return NotFound();
            }

            var response = _heistService.GetEligibleMembers(heist);

            return Ok(response);
        }
    }
}
