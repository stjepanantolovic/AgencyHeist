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
        public MemberService(DataContext context)
        {
            _context = context;
        }

        public Member GetMember(Guid id)
        {
            return _context.Members.Where(m => m.ID == id).Include(m => m.SkillLevels).Include(m => m.MainSkill).FirstOrDefault();
        }

        public Skill GetSkill(string skillName)
        {
            if (skillName == null)
            {
                return null;
            }
            return _context.Skill.Where(s => s.Name == skillName).FirstOrDefault();
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

        public List<SkillResponse> GetMemberSkills(ICollection<SkillLevel> skillLevels)
        {
            var response = new List<SkillResponse>();
            var skillIDs = skillLevels.Select(m => m.SkillID);
            var levelIDs = skillLevels.Select(m => m.LevelID);
            var memberSkills = _context.Skill.Where(s => skillIDs.Contains(s.ID)).ToList();
            var memberLevels = _context.Levels.Where(l => levelIDs.Contains(l.ID)).ToList();
            foreach (var skillLevel in skillLevels)
            {
                var skillName = memberSkills.Where((s => s.ID == skillLevel.SkillID)).Select(s => s.Name).FirstOrDefault();
                var levelValue = memberLevels.Where((s => s.ID == skillLevel.LevelID)).Select(s => s.Value).FirstOrDefault();
                var memberSkill = new SkillResponse()
                {
                    Name = skillName,
                    Level = levelValue
                };
                response.Add(memberSkill);
            }
            return response;
        }

        public void UpdateMemberSkills(Member member, UpdateMemberSkillsRequest request)
        {
            if (string.IsNullOrEmpty(request.MainSkill) && (request?.Skills == null || request.Skills.Count == 0))
            {
                throw new HeistException($"Main skill or skilss should be provided when updating member skills");
            }

            var mainSkillChanges = MemberHelperService.MemberMainSkillChanges(member?.MainSkill?.Name, request.MainSkill);
            var memberSkills = _context.Skill.Where(sk => member.SkillLevels.Select(sl => sl.SkillID).ToList().Contains(sk.ID)).ToList();
            if (mainSkillChanges && !MemberHelperService.SkillsContainsMainSkill(request.Skills, memberSkills, request?.MainSkill))
            {
                var userMessage = $"When Changing Member's main skill, Array of Skills in request shoud contain request's Main skill";
                var systemMessage = $"[MemberService.UpdateMemberSkills] => When Changing Member's main skill, Array of Skills in request shoud contain request's Main skill. " +
                                    $"UpdateMemberSkillsRequest: {JsonConvert.SerializeObject(request)}";
                throw new HeistException(userMessage, systemMessage);
            }
            if (member.SkillLevels == null)
            {
                member.SkillLevels = new List<SkillLevel>();
            }
            var skillLevels = GetSkillLevelsFromSkillRequest(request.Skills);
            if (skillLevels != null)
            {
                MemberHelperService.UpdateMemberSkillLevels(member, skillLevels.ToList());
            }

            if (mainSkillChanges)
            {
                member.MainSkillID = _context.Skill.Where(s => s.Name == request.MainSkill).Select(s => s.ID).FirstOrDefault();
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
            if (MemberHelperService.CheckForDoublesInSkillRequestList(memberRequest.Skills.ToList()))
            {
                throw new Exception($"Member can not have two skill with same name");
            }
            var upsertedSkillLevels = GetSkillLevelsFromSkillRequest(memberRequest.Skills);
            var test = "";
            var mainSkill = _context.Skill.Where(s => s.Name == memberRequest.MainSkill).FirstOrDefault();
            var member = new Member()
            {
                Name = memberRequest?.Name,
                Email = memberRequest?.Email,
                MainSkillID = mainSkill.ID,
                Sex = GetSex(memberRequest?.Name),
                SkillLevels = upsertedSkillLevels,
                Status = GetMemberStatus(memberRequest?.Status)
            };
            _context.Members?.Add(member);
            return member;
        }

        public static Skill CreateSkill(SkillRequest skillRequest)
        {
            return new Skill()
            {
                Name = skillRequest.Name
            };
        }


        public ICollection<Skill>? AddSkillsToDb(ICollection<SkillRequest>? skillRequests)
        {
            var response = new List<Skill>();

            if (skillRequests == null)
            {
                return null;
            }
            var levels = _context.Levels.Where(x => skillRequests.Select(x => x.Level).Contains(x.Value)).ToList();

            foreach (var request in skillRequests)
            {
                var level = levels.Where(l => l.Value == request.Level).FirstOrDefault();

                response.Add(
                    CreateSkill(request)
                    );
            }
            _context.Skill?.AddRange(response);
            _context.SaveChanges();
            return response;
        }
        public ICollection<SkillLevel>? GetSkillLevelsFromSkillRequest(ICollection<SkillRequest>? skillRequests)
        {
            if (skillRequests == null)
            {
                return null;
            }
            var existingSkills = _context.Skill.Where(s => skillRequests.Select(sr => sr.Name).ToList().Contains(s.Name)).ToList();
            if (existingSkills == null)
            {
                existingSkills = new List<Skill>();
            }
            var skillReqsToBeAdded = skillRequests.Where(sr => !existingSkills.Select(es => es.Name).ToList().Contains(sr.Name)).ToList();

            if (skillReqsToBeAdded != null && skillReqsToBeAdded.Count > 0)
            {
                existingSkills.AddRange(AddSkillsToDb(skillReqsToBeAdded));
            }
            return UpsertSkillLevels(existingSkills, skillRequests.ToList());


        }

       


        public Sex? GetSex(string? name)
        {
            return name == null ? null : _context.Sex?.Where(s => s.Name == name).FirstOrDefault();
        }

        public MemberStatus? GetMemberStatus(string? name)
        {
            return name == null ? null : _context.MemberStatus.Where(s => s.Name == name).FirstOrDefault();
        }

        public List<SkillLevel> UpsertSkillLevels(List<Skill> skills, List<SkillRequest> skillRequests)
        {
            var response = new List<SkillLevel>();
            var skillIDs = skills.Select(x => x.ID).ToList();

            var levels = _context.Levels.Where(x => skillRequests.Select(x => x.Level).Contains(x.Value)).ToList();

            var existingSkillLevles = _context.SkillLevels.Where(sl => skillIDs.Contains(sl.SkillID)).ToList();

            var skillLevelsToAdd = new List<SkillLevel>();

            foreach (var request in skillRequests)
            {
                var levelID = levels.Where(l => l.Value == request.Level).Select(l => l.ID).FirstOrDefault();
                if (levelID == null || levelID == Guid.Empty)
                {
                    var userMessage = $"Received level {request.Level} is not allowed";
                    throw new HeistException(userMessage, $"MemberService.UpsertSkillLevel=> {userMessage}");
                }
                var skillID = skills.Where(s => s.Name == request.Name).Select(s => s.ID).FirstOrDefault();
                var skillLevel = existingSkillLevles.Where(esl => esl.SkillID == skillID && esl.LevelID == levelID).FirstOrDefault();
                if (skillLevel == null)
                {
                    skillLevel = new SkillLevel() { LevelID = levelID, SkillID = skillID };
                    skillLevelsToAdd.Add(skillLevel);
                }
                else
                {
                    response.Add(skillLevel);
                }
            }

            _context.SkillLevels.AddRange(skillLevelsToAdd);
            _context.SaveChanges();
            response.AddRange(skillLevelsToAdd);
            return response;
        }

        public List<Skill> GetSkillsFromSkillRequests(List<SkillRequest> request)
        {
            var skillNames = request.Select(s => s.Name).ToList();
            return _context.Skill.Where(s => skillNames.Contains(s.Name)).ToList();
        }

        public List<Level>? GetLevelsFromSkillRequests(List<SkillRequest> request)
        {
            var levelValues = request.Select(s => s.Level).ToList();

            return levelValues == null ? null : _context.Levels.Where(l => levelValues.Contains(l.Value)).ToList();


        }
    }
}
