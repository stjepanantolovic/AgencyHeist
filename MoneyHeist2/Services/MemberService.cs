using MoneyHeist2.Data;
using MoneyHeist2.Data.Repos;
using MoneyHeist2.Entities;
using MoneyHeist2.Entities.DTOs;

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
            var member = new Member()
            {
                Name = memberRequest.Name,
                Email = memberRequest.Email,
                MainSkill = memberRequest?.MainSkill == null ? null : new Skill()
                {
                    Name = memberRequest.MainSkill,
                    SkillLevels = new List<SkillLevel>() { new SkillLevel()
                    { Value =  memberRequest.Skills.Where(s => s.Name == memberRequest.MainSkill).Select(l => l.Level).FirstOrDefault() }}

                },
                Sex = GetSex(memberRequest?.Name),
                Skills = AddSkillsToDb(memberRequest?.Skills),
                Status = GetMemberStatus(memberRequest?.Status)
            };
            _context.Members.Add(member);
            return member;
        }

        public static Skill CreateSkill(SkillRequest skillRequest, SkillLevel skillLevel)
        {
            return new Skill()
            {
                Name = skillRequest.Name,
                SkillLevels = new List<SkillLevel>() { skillLevel }                
            };
        }

        //public ICollection<SkillLevel> UpsertSkillLevels(ICollection<string> skillLevelValues)
        //{
        //    var existingSkillLevels = _context.SkillLevels.Where(sl => skillLevelValues.Contains(sl.Value)).ToList();
        //    var skillLevelsToBeAdded = skillLevelValues.Where(slv => !existingSkillLevels.Select(es => es.Value).Contains(slv)).ToList();
        //    if (skillLevelsToBeAdded!=null)
        //    {                
        //        existingSkillLevels.AddRange(AddSkillLevelsToDb(skillLevelsToBeAdded));
        //    }
        //    return existingSkillLevels;

        //}

        //public List<SkillLevel> AddSkillLevelsToDb(List<string> skillLevelValues)
        //{
        //    var newSkillLevels = new List<SkillLevel>();
        //    foreach (var value in skillLevelValues)
        //    {
        //        newSkillLevels.Add(new SkillLevel()
        //        {
        //            Value = value
        //        });
        //    }
        //    _context.AddRange(newSkillLevels);
        //    return newSkillLevels;
        //}

        public ICollection<Skill>? AddSkillsToDb(ICollection<SkillRequest>? skillRequests)
        {
            var skillLevels = _context.SkillLevels.ToList();
            if (skillRequests == null)
            {
                return null;
            }

            var skills = new List<Skill>();
            foreach (var request in skillRequests)
            {
                var skillLevel = skillLevels.Where(sl => sl.Value == request.Level).FirstOrDefault();
                if (skillLevel == null)
                {
                    throw new Exception($"Skill level {request.Level} is not valid!");
                }
                skills.Add(
                    CreateSkill(request, skillLevel)
                    );
            }
            _context.Skill.AddRange(skills);
            return skills;
        }
        public ICollection<Skill>? UpsertSkillsToDb(ICollection<SkillRequest>? skillRequests)
        {
            if (skillRequests == null)
            {
                return null;
            }
            var existingSkills = _context.Skill.Where(s => skillRequests.Select(sr => sr.Name).ToList().Contains(s.Name)).ToList();            
            var skillReqsToBeAdded = skillRequests.Where(sr => !existingSkills.Select(es => es.Name).ToList().Contains(sr.Name)).ToList();
            existingSkills.AddRange(AddSkillsToDb(skillReqsToBeAdded));
            return existingSkills;
        }
        

        public Sex? GetSex(string? name)
        {
            return name == null ? null : _context.Sex.Where(s => s.Name == name).FirstOrDefault();
        }

        public MemberStatus? GetMemberStatus(string? name)
        {
            return name == null ? null : _context.MemberStatus.Where(s => s.Name == name).FirstOrDefault();
        }
    }
}
