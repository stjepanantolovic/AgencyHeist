using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyHeist2.Data.Repos;
using MoneyHeist2.Entities.DTOs;
using MoneyHeist2.Exceptions;
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

        [Route("createMember")]
        [HttpPost]        
        public IActionResult CreateMember(MemberRequest memberRequest)
        {
            try
            {
                _memberService.CreateMember(memberRequest);
                if (_repo.SaveAll())
                    return Created("", 1);
                return BadRequest("System is currently unavailable");
            }
            catch (HeistException hex)
            {

                //Log hex.Message
                return BadRequest(hex.UserMessage);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            

            
        }
    }
}
