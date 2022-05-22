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
        private readonly MemberService _memberService;

        public HeistService(DataContext context, MemberService memberService)
        {
            _memberService = memberService;
            _context = context;
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
            response.HeistSkillLevels = GetHeistSkillLevels(request.Skills);
            _context.Heists.Add(response);
            SaveAll();
            return response;
        }

        public List<HeistSkillLevel> GetHeistSkillLevels(List<HeistSkillRequest> heistSkillRequests)
        {
            var response = new List<HeistSkillLevel>();
            var skillRequests = heistSkillRequests.Select(x => new SkillRequest() { Name = x.Name, Level = x.Level }).ToList();
            var skillLevels = _memberService.GetSkillLevelsFromSkillRequest(skillRequests);

            var skillsFromRequest = _memberService.GetSkillsFromSkillRequests(skillRequests);
            var levelsFromRequest = _memberService.GetLevelsFromSkillRequests(skillRequests);

            foreach (var heistSkillRequest in heistSkillRequests)
            {
                var skillID = skillsFromRequest.Where(s => s.Name == heistSkillRequest.Name).Select(s => s.ID).FirstOrDefault();
                var levelID = levelsFromRequest.Where(l => l.Value == heistSkillRequest.Level).Select(l => l.ID).FirstOrDefault();
                var skillLevel = skillLevels.Where(sl => sl.SkillID == skillID && sl.LevelID == levelID).FirstOrDefault();
                var heistSkillLevel = new HeistSkillLevel() { SkillLevelID = skillLevel.ID, Members = heistSkillRequest.Members };
                response.Add(heistSkillLevel);
            }
            _context.HeistSkillLevels.AddRange(response);
            SaveAll();
            return response;
        }

        public bool IsHeistRequestOk(HeistRequest heistRequest)
        {
            if (HeistHelperService.ListHasDoubles(heistRequest?.Skills?.ToList()))
            {
                throw new HeistException($"Request contains double skills with same name and level property");
            }

            if (!HeistHelperService.AreDatesOK(heistRequest.StartTime, heistRequest.EndTime))
            {
                throw new HeistException($"Start Time {heistRequest.StartTime} should not be in the past and endTime {heistRequest.EndTime} should be after startTime");
            }
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
