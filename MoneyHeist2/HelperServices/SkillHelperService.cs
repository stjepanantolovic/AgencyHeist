using MoneyHeist2.Entities;
using MoneyHeist2.Entities.DTOs;
using MoneyHeist2.Entities.DTOs.Heist;
using MoneyHeist2.Exceptions;

namespace MoneyHeist2.HelperServices
{
    public static class SkillHelperService
    {
        public static void CheckForDoublesInList(List<SkillRequest> list)
        {            
            if (list != null && list.GroupBy(x => new { x.Name })
                   .Where(x => x.Skip(1).Any()).Any())
            {
                throw new HeistException($"Member can not have two skill with same name");
            }
        }

        public static void CheckForDoublesInList(List<HeistSkillRequest> list)
        {
            if (list != null && list.GroupBy(x => new { x.Name, x.Level })
                   .Where(x => x.Skip(1).Any()).Any())
            {
                throw new HeistException($"Request contains double skills with same name and level property");
            }
        }

        public static bool MemberMainSkillChanges(string? mainSkill, string? newMainSkill)
        {
            return !string.IsNullOrEmpty(newMainSkill) && mainSkill != newMainSkill;
        }

        public static bool SkillsContainsMainSkill(List<SkillRequest> skills, List<Skill> memberSkills, string? mainSkill)
        {
            return !string.IsNullOrEmpty(mainSkill)
                && (skills.Select(s => s.Name).ToList().Contains(mainSkill) || memberSkills.Select(ms => ms.Name).Contains(mainSkill));
        }
    }
}
