using MoneyHeist2.Entities;
using MoneyHeist2.Entities.DTOs;

namespace MoneyHeist2.HelperServices
{
    public static class MemberHelperService
    {   
        public static void UpdateMemberSkillLevels(Member member, List<SkillLevel> updatedMemberSkillLevels)
        {
            foreach (var updateSkillLevel in updatedMemberSkillLevels)
            {
                var existingMemberSkillLevel = member.SkillLevels.Where(sl => sl.ID == updateSkillLevel.ID).FirstOrDefault();
                if (existingMemberSkillLevel == null)
                {
                    var existingSkillLevelBySkill = member.SkillLevels.Where(msl => msl.SkillID == updateSkillLevel.SkillID).FirstOrDefault();
                    if (existingSkillLevelBySkill != null)
                    {
                        member.SkillLevels.Remove(existingSkillLevelBySkill);
                    }
                    member.SkillLevels.Add(updateSkillLevel);
                }
            }
        }       
    }
}
