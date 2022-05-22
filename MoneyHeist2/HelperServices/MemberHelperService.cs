using MoneyHeist2.Entities;
using MoneyHeist2.Entities.DTOs;

namespace MoneyHeist2.HelperServices
{
    public static class MemberHelperService
    {
        public static bool MemberMainSkillChanges(string? mainSkill, string? newMainSkill)
        {
            return !string.IsNullOrEmpty(newMainSkill) && mainSkill != newMainSkill;
        }

        public static bool SkillsContainsMainSkill(List<SkillRequest> skills, List<Skill> memberSkills, string? mainSkill)
        {
            return !string.IsNullOrEmpty(mainSkill)
                && (skills.Select(s => s.Name).ToList().Contains(mainSkill) || memberSkills.Select(ms => ms.Name).Contains(mainSkill));
        }

        public static void UpdateMemberSkillLevels(Member member, List<SkillLevel> updatedmemberSkillLevels)
        {
            foreach (var updateSkillLevel in updatedmemberSkillLevels)
            {
                var existingMemberSkillLevel = member.SkillLevels.Where(sl => sl.ID == updateSkillLevel.ID).FirstOrDefault();
                if (existingMemberSkillLevel == null)
                {
                    var existingSkillLevelByName = member.SkillLevels.Where(msl => msl.SkillID == updateSkillLevel.SkillID).FirstOrDefault();
                    if (existingSkillLevelByName != null)
                    {
                        member.SkillLevels.Remove(existingSkillLevelByName);
                    }
                    member.SkillLevels.Add(updateSkillLevel);
                }
            }
        }

        public static bool CheckForDoublesInSkillRequestList(List<SkillRequest> list)
        {
            return list.GroupBy(x => new { x.Name })
                   .Where(x => x.Skip(1).Any()).Any();


        }
    }
}
