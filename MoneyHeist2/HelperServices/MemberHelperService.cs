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
                    var existingSkillLevelByName = member.SkillLevels.Where(msl => msl.SkillID == updateSkillLevel.SkillID).FirstOrDefault();
                    if (existingSkillLevelByName != null)
                    {
                        member.SkillLevels.Remove(existingSkillLevelByName);
                    }
                    member.SkillLevels.Add(updateSkillLevel);
                }
            }
        }       
    }
}
