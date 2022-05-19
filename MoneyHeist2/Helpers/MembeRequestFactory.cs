using MoneyHeist2.Data.Repos;
using MoneyHeist2.Entities;
using MoneyHeist2.Entities.DTOs;

namespace MoneyHeist2.Helpers
{
    public static class MembeRequestFactory
    {    
        //public static Member CreateMember(MemberRequest memberRequest, IHeistRepository _repo)
        //{
        //    //return new Member()
        //    //{
        //    //    Name = memberRequest.Name,
        //    //    Email = memberRequest.Email,
        //    //    MainSkill = memberRequest?.MainSkill == null ? null : new Skill()
        //    //    {
        //    //        Name = memberRequest.MainSkill,
        //    //        SkillLevels = new List<Level>() { new Level()
        //    //        { Value =  memberRequest.Skills.Where(s => s.Name == memberRequest.MainSkill).Select(l => l.Level).FirstOrDefault() }}

        //    //    },
        //    //    Sex = CreateSex(memberRequest?.Name),
        //    //    Skills = CreateSkills(memberRequest?.Skills),
        //    //    Status = new MemberStatus() { Name = memberRequest?.Status }

        //    //};
        //}

        public static Skill CreateSkill(SkillRequest skillRequest)
        {
            return new Skill()
            {
                Name = skillRequest.Name,
                //SkillLevels = new List<Level>() { CreateSkillLevel(skillRequest.Level) }
            };
        }

        public static Level CreateSkillLevel(string level)
        {
            return new Level()
            {
                Value = level
            };
        }

        public static ICollection<Skill>? CreateSkills(ICollection<SkillRequest>? skillRequest)
        {
            if (skillRequest == null)
            {
                return null;
            }
            var skills = new List<Skill>();
            foreach (var request in skillRequest)
            {
                skills.Add(
                    CreateSkill(request)
                    );
            }
            return skills;
        }

        public static Sex? CreateSex(string? name)
        {
            return name == null ? null : new Sex() { Name = name };
        }
    }
}




