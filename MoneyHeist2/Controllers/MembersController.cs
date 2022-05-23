using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyHeist2.Data.Repos;
using MoneyHeist2.Entities.DTOs;
using MoneyHeist2.Exceptions;
using MoneyHeist2.Helpers;
using MoneyHeist2.Services;
using System.Net;

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
                var member = _memberService.CreateMember(memberRequest);
                if (_repo.SaveAll())
                    return CreatedAtRoute(routeName: "GetMember",
            routeValues: new { id = member.ID },
            value: null);
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

        [HttpGet("{id}", Name = "GetMember")]
        public IActionResult GetMember(Guid id)
        {
            try
            {
                var member = _memberService.GetMember(id);

                if (member == null)
                    return NotFound();

                return Ok(member);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{member_id}/skills")]
        public IActionResult UpdateMemberSkills(Guid member_id, UpdateMemberSkillsRequest request)
        {
            try
            {
                var member = _memberService.GetMemberWithSkills(member_id);

                if (member == null)
                    return NotFound();
                _memberService.UpdateMemberSkills(member, request);

                var response = new ContentResult() { StatusCode = 204, };
                var locationUrl = $"{member_id}/skills";
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
        [Route("{member_id}/skills")]
        public IActionResult GetMemberSkills(Guid member_id)
        {            
            var member = _memberService.GetMemberWithSkills(member_id);

            if (member == null)
            {
                return NotFound();
            }

            var response = new MemberSkillsResponse() { MainSkill=member.MainSkill.Name};
            
            response.Skills = _memberService.GetSkillResponsFromMemberSkillLevels(member.SkillLevels.ToList());

            return Ok(response);
        }

        [HttpDelete]
        [Route("{member_id}/skills/{skill_name}")]
        public IActionResult RemoveMemberSkills(Guid member_id, string skill_name)
        {
            var member = _memberService.GetMemberWithSkills(member_id);

            if (member == null)
            {
                return NotFound("Member does not exist ");
            }

            var skillToRemove = _memberService.GetMemberSkill(skill_name);

            if (skillToRemove== null || !member.SkillLevels.Select(sl=>sl.SkillID).Contains(skillToRemove.ID))
            {
                return NotFound();
            }
            return NoContent();
        }




    }
}
