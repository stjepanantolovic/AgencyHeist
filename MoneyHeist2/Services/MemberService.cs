using MoneyHeist2.Data;
using MoneyHeist2.Data.Repos;
using MoneyHeist2.Entities;
using MoneyHeist2.Entities.DTOs;
using MoneyHeist2.Exceptions;

namespace MoneyHeist2.Services
{
    public class MemberService
    {
        private readonly DataContext _context;
        public MemberService(DataContext context)
        {
            _context = context;
        }

        public Member CreateMember(MemberRequest memberRequest)
        {
            var existingMember = _context.Members.Where(m => m.Email == memberRequest.Email).FirstOrDefault();
            if (existingMember != null)
            {
                throw new Exception($"Member with email {memberRequest.Email} already exists in database");
            }
            if(CheckForDoublesInList(memberRequest.Skills.ToList(), "Name"))
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

            if (skillReqsToBeAdded != null && skillReqsToBeAdded.Count>0)
            {
                existingSkills.AddRange(AddSkillsToDb(skillReqsToBeAdded));
            }
            return UpsertSkillLevels(existingSkills, skillRequests.ToList());


        }

        public bool CheckForDoublesInList(List<SkillRequest> list, string propertyName)
        {
            return list.GetType().GetProperties()
                    .Where(property => property.Name.StartsWith(propertyName))
                    .Count(property => property.GetValue(this, null) != null) >= 1;

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
            var skillIDs = skills.Select(x=>x.ID).ToList();            
          
            var levels = _context.Levels.Where(x=> skillRequests.Select(x=>x.Level).Contains(x.Value)).ToList();

            var existingSkillLevles = _context.SkillLevels.Where(sl => skillIDs.Contains(sl.SkillID)).ToList();
            
           var skillLevelsToAdd = new List<SkillLevel>();
                       
            foreach (var request in skillRequests)
            {
                var levelID = levels.Where(l => l.Value == request.Level).Select(l => l.ID).FirstOrDefault();
                if (levelID == null || levelID== Guid.Empty)
                {
                    var userMessage = $"Received level {request.Level} is not allowed";
                    throw new HeistException(userMessage, $"MemberService.UpsertSkillLevel=> {userMessage}");
                }
                var skillID = skills.Where(s => s.Name == request.Name).Select(s=>s.ID).FirstOrDefault();
                var skillLevel = existingSkillLevles.Where(esl => esl.SkillID == skillID && esl.LevelID == levelID).FirstOrDefault();
                if(skillLevel == null)
                {
                    skillLevel = new SkillLevel() { LevelID=levelID, SkillID=skillID};
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
    }
}
