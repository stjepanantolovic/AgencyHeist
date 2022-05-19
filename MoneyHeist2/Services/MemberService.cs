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
            var existingMember = _context.Members.Where(m => m.Email == memberRequest.Email).FirstOrDefault();
            if (existingMember != null)
            {
                throw new Exception($"Member with email {memberRequest.Email} already exists in database");
            }
            var upsertedSkillLevels = UpsertSkillsToDb(memberRequest.Skills);
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

            if (skillRequests == null)
            {
                return null;
            }

            var skills = new List<Skill>();
            foreach (var request in skillRequests)
            {
                skills.Add(
                    CreateSkill(request)
                    );
            }
            _context.Skill?.AddRange(skills);
            _context.SaveChanges();
            return skills;
        }
        public ICollection<SkillLevel>? UpsertSkillsToDb(ICollection<SkillRequest>? skillRequests)
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

            if (skillReqsToBeAdded != null)
            {
                existingSkills.AddRange(AddSkillsToDb(skillReqsToBeAdded));
            }
            return UpsertSkillSkillLevels(existingSkills, skillRequests.ToList());


        }


        public Sex? GetSex(string? name)
        {
            return name == null ? null : _context.Sex?.Where(s => s.Name == name).FirstOrDefault();
        }

        public MemberStatus? GetMemberStatus(string? name)
        {
            return name == null ? null : _context.MemberStatus.Where(s => s.Name == name).FirstOrDefault();
        }

        public List<SkillLevel> UpsertSkillSkillLevels(List<Skill> skills, List<SkillRequest> skillRequests)
        {
            var skillIDs = skills.Select(x=>x.ID).ToList();            
            var skillLevelsWithSkillIDs = _context.SkillLevels.Where(sl => skillIDs.Contains(sl.SkillID)).ToList();
            if (skillLevelsWithSkillIDs == null)
            {
                skillLevelsWithSkillIDs = new List<SkillLevel>();
            }
            
            var levels = _context.Levels.Where(x=> skillRequests.Select(x=>x.Level).Contains(x.Value)).ToList();

            var existingSkillLevels = skillLevelsWithSkillIDs.Where(x => levels.Select(x=>x.ID).Contains(x.LevelID)).ToList();            

            var skillIDToBeAdded = skillIDs.Where(si => !(existingSkillLevels.Select(sl => sl.SkillID).Contains(si))).ToList();

            var skillLevelsToAdd = new List<SkillLevel>();
            foreach (var skillID in skillIDToBeAdded)
            {
                var skill = skills.Where(s => s.ID == skillID).FirstOrDefault();
                var skillRequest = skillRequests.Where(sr => sr.Name == skill.Name).FirstOrDefault();
                var level = levels.Where(l => l.Value == skillRequest.Level).FirstOrDefault();
                var skillLevel = new SkillLevel() { SkillID=skill.ID, LevelID=level.ID};
                skillLevelsToAdd.Add(skillLevel);
            }            

            _context.SkillLevels.AddRange(skillLevelsToAdd);
            _context.SaveChanges();
            existingSkillLevels.AddRange(skillLevelsToAdd);
            return existingSkillLevels;
        }

        public static bool ListsAreNotEmpty(List<List<dynamic>> lists)
        {
            foreach (var list in lists)
            {
                if (!(list != null && list.Count > 0))
                {
                    return false;
                }

            }
            return true;
        }
    }
}
