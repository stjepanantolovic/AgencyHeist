using MoneyHeist2.Exceptions;
using Microsoft.AspNetCore.Mvc;
using MoneyHeist2.Entities.DTOs;
using MoneyHeist2.Entities.DTOs.Heist;
using MoneyHeist2.Entities;

namespace MoneyHeist2.HelperServices
{
    public static class HeistHelperService
    {      

        public static void ChekcHeistDates(DateTime? startTime, DateTime? endTime)
        {
            var datesAreOk =  startTime != null && endTime != null
                && startTime >= DateTime.Now
                && startTime <= endTime;

            if (!datesAreOk)
            {
                throw new HeistException($"Start Time {startTime} should not be in the past and endTime {endTime} should be after startTime");
            }
        }

        public static void UpdateHeistSkillLevels(Heist heist, List<HeistSkillLevel> updatedHeistSkillLevels)
        {
            foreach (var updateHeistSkillLevel in updatedHeistSkillLevels)
            {
                var existingHeistSkillLevel = heist.HeistSkillLevels.Where(sl => sl.ID == updateHeistSkillLevel.ID).FirstOrDefault();
                if (existingHeistSkillLevel == null)
                {
                    var existingSkillLevelByName = heist.HeistSkillLevels.Where(hsl => hsl.SkillLevelID == updateHeistSkillLevel.SkillLevelID).FirstOrDefault();
                    if (existingSkillLevelByName != null)
                    {
                        heist.HeistSkillLevels.Remove(existingSkillLevelByName);
                    }
                    heist.HeistSkillLevels.Add(updateHeistSkillLevel);
                }
            }
        }
    }
}
