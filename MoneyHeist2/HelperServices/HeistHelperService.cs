using MoneyHeist2.Exceptions;
using Microsoft.AspNetCore.Mvc;
using MoneyHeist2.Entities.DTOs;
using MoneyHeist2.Entities.DTOs.Heist;

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
    }
}
