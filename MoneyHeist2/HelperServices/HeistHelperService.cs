using MoneyHeist2.Exceptions;
using Microsoft.AspNetCore.Mvc;
using MoneyHeist2.Entities.DTOs;
using MoneyHeist2.Entities.DTOs.Heist;

namespace MoneyHeist2.HelperServices
{
    public static class HeistHelperService
    {
        public static bool ListHasDoubles(List<HeistSkillRequest> list)
        {
            return list.GroupBy(x => new { x.Name, x.Level })
                   .Where(x => x.Skip(1).Any()).Any();
        }

        public static bool AreDatesOK(DateTime? startTime, DateTime? endTime)
        {
            return startTime != null && endTime != null
                && startTime >= DateTime.Now
                && startTime <= endTime;

        }
    }
}
