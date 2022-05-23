using Microsoft.EntityFrameworkCore;
using MoneyHeist2.Data;
using MoneyHeist2.Data.Repos;
using MoneyHeist2.Entities;
using MoneyHeist2.Entities.DTOs;
using MoneyHeist2.Exceptions;
using MoneyHeist2.HelperServices;
using Newtonsoft.Json;

namespace MoneyHeist2.Services
{
    public class MemberService
    {
        private readonly DataContext _context;
        private readonly SkillService _skillService;
        public MemberService(DataContext context, SkillService skillService)
        {
            _context = context;
            _skillService = skillService;
        }

        public Member GetMemberWithSkills(Guid id)
        {
            return _context.Members.Where(m => m.ID == id).Include(m => m.SkillLevels).Include(m => m.MainSkill).FirstOrDefault();
        }

        public Member GetMember(Guid id)
        {
            return _context.Members.Where(m => m.ID == id).Include(m => m.MainSkill).Include(m=>m.Sex).Include(m=>m.Status).FirstOrDefault();
        }

        public void RemoveMeberSkill(Member member, Skill skillToRemove)
        {
            var memberSkillLevelToRemove = member.SkillLevels.Where(sl => sl.SkillID == skillToRemove.ID).FirstOrDefault();
            if (memberSkillLevelToRemove != null) { member.SkillLevels.Remove(memberSkillLevelToRemove); }
            if (member.MainSkillID == skillToRemove.ID)
            {
                member.MainSkillID = null;
                member.MainSkill = null;
            }

            _context.Members.Update(member);
            _context.SaveChanges();

        }

        public List<SkillResponse>? GetSkillResponsFromMemberSkillLevels(List<SkillLevel>? skillLevels)
        {
            if (skillLevels==null)
            {
                return null;
            }
            return _skillService.GetSkillResponsFromSkillLevels(skillLevels);
        }

        public Skill? GetMemberSkill(string? skillName)
        {
            if (skillName == null)
            {
                return null;
            }
            return _skillService.GetSkill(skillName);
        }

        public void UpdateMemberSkills(Member member, UpdateMemberSkillsRequest request)
        {
            if (string.IsNullOrEmpty(request.MainSkill) && (request?.Skills == null || request.Skills.Count == 0))
            {
                throw new HeistException($"Main skill or skilss should be provided when updating member skills");
            }
                        
            if (SkillHelperService.MemberMainSkillChanges(member?.MainSkill?.Name, request.MainSkill))
            {
                var memberSkills = _context.Skill.Where(sk => member.SkillLevels.Select(sl => sl.SkillID).ToList().Contains(sk.ID)).ToList();
                if (!SkillHelperService.SkillsContainsMainSkill(request.Skills, memberSkills, request?.MainSkill))
                {
                    var userMessage = $"When Changing Member's main skill, Array of Skills in request shoud contain request's Main skill";
                    var systemMessage = $"[MemberService.UpdateMemberSkills] => When Changing Member's main skill, Array of Skills in request shoud contain request's Main skill. " +
                                        $"UpdateMemberSkillsRequest: {JsonConvert.SerializeObject(request)}";
                    throw new HeistException(userMessage, systemMessage);
                }
                member.MainSkillID = _context.Skill.Where(s => s.Name == request.MainSkill).Select(s => s.ID).FirstOrDefault();
            }
            
            
            if (member.SkillLevels == null)
            {
                member.SkillLevels = new List<SkillLevel>();
            }
            var skillLevels = _skillService.GetSkillLevelsFromSkillRequest(request.Skills);
            if (skillLevels != null)
            {
                MemberHelperService.UpdateMemberSkillLevels(member, skillLevels.ToList());
            }

           
            _context.Members.Update(member);
            _context.SaveChanges();
        }

        public Member CreateMember(MemberRequest memberRequest)
        {
            var existingMember = _context.Members.Where(m => m.Email == memberRequest.Email).FirstOrDefault();
            if (existingMember != null)
            {
                throw new Exception($"Member with email {memberRequest.Email} already exists in database");
            }
            SkillHelperService.CheckForDoublesInList(memberRequest.Skills.ToList());
            
            var upsertedSkillLevels = _skillService.GetSkillLevelsFromSkillRequest(memberRequest.Skills);
            var test = "";
            var mainSkill = _context.Skill.Where(s => s.Name == memberRequest.MainSkill).FirstOrDefault();
            var member = new Member()
            {
                Name = memberRequest?.Name,
                Email = memberRequest?.Email,
                MainSkillID = mainSkill.ID,
                Sex = GetSex(memberRequest?.Sex),
                SkillLevels = upsertedSkillLevels,
                Status = GetMemberStatus(memberRequest?.Status)
            };
            _context.Members?.Add(member);
            return member;
        }

        public Sex? GetSex(string? name)
        {
            return name == null ? null : _context.Sex?.Where(s => s.Name == name).FirstOrDefault();
        }

        public MemberStatus? GetMemberStatus(string? name)
        {
            return name == null ? null : _context.MemberStatus.Where(s => s.Name == name).FirstOrDefault();
        }
    }
}
