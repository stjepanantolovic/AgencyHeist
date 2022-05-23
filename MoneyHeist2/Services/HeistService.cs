using Microsoft.EntityFrameworkCore;
using MoneyHeist2.Data;
using MoneyHeist2.Entities;
using MoneyHeist2.Entities.DTOs;
using MoneyHeist2.Entities.DTOs.Heist;
using MoneyHeist2.Exceptions;
using MoneyHeist2.HelperServices;

namespace MoneyHeist2.Services
{

    public class HeistService
    {
        private readonly DataContext _context;
        private readonly SkillService _skillService;

        public HeistService(DataContext context,  SkillService skillService)
        {           
            _context = context;
            _skillService = skillService;
        }

        public Heist GetHeist(Guid id)
        {
            return _context.Heists.Where(h => h.ID == id).Include(h => h.HeistSkillLevels).FirstOrDefault();
        }

        public Heist CreateHeist(HeistRequest request)
        {
            IsHeistRequestOk(request);

            var response = new Heist() { Name = request.Name, Location = request.Location, StartTime = request.StartTime, EndTime = request.EndTime };
            var skillRequests = request.Skills.Select(x => new SkillRequest() { Name = x.Name, Level = x.Level }).ToList();
            response.HeistSkillLevels = _skillService.GetHeistSkillLevels(request.Skills);
            _context.Heists.Add(response);
            SaveAll();
            return response;
        }              

        public bool IsHeistRequestOk(HeistRequest heistRequest)
        {
            SkillHelperService.CheckForDoublesInList(heistRequest?.Skills?.ToList());
            HeistHelperService.ChekcHeistDates(heistRequest.StartTime, heistRequest.EndTime);
           
            var isHeistNametaken = _context.Heists.Any(h => h.Name == heistRequest.Name);
            if (isHeistNametaken)
            {
                throw new HeistException($"Heist with Name {heistRequest.Name} is already taken");
            }
            return true;
        }

        public bool SaveAll()
        {
            if (!(_context.SaveChanges() > 0))
            {
                var userMessage = $"Error occured while saving Skills to database";
                var systemMessage = $"HeistService.GetHeistSkillLevels => Unable to save HeistSkillLevels to database";
                throw new HeistException(userMessage, systemMessage);
            }
            return true;
        }

    }
}
