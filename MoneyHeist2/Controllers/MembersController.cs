using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyHeist2.Data.Repos;
using MoneyHeist2.Entities.DTOs;
using MoneyHeist2.Helpers;
using MoneyHeist2.Services;

namespace MoneyHeist2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IHeistRepository _repo;
        private readonly MemberService _memberService;
        public MembersController(IHeistRepository repo, MemberService memberService)
        {
            _repo = repo;
            _memberService = memberService;
        }

        [HttpGet("{id}", Name = "GetMember")]
        public IActionResult GetMember(Guid id)
        {
            try
            {
                var member = _repo.GetMember(id);

                if (member == null)
                    return NotFound();

                return Ok(member);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }



        }

        [Route("createMember")]
        [HttpPost]
        public IActionResult CreateMember(MemberRequest memberRequest)
        {
            try
            {
                var member = _memberService.CreateMember(memberRequest);
                if (_repo.SaveAll())
                    return CreatedAtRoute("GetMember", new { id = member.ID }, member);

                return BadRequest("System is currently unavailable");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


        }
    }
}
