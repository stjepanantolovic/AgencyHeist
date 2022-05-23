using MoneyHeist2.Data;
using MoneyHeist2.Entities;
using MoneyHeist2.Entities.DTOs;
using MoneyHeist2.Entities.DTOs.Heist;
using MoneyHeist2.Exceptions;

namespace MoneyHeist2.Services
{
    public class SkillService
    {
        private readonly DataContext _context;
        public SkillService(DataContext context)
        {
            _context = context;
        }

        public List<SkillResponse> GetSkillResponsFromSkillLevels(ICollection<SkillLevel> skillLevels)
        {
            var response = new List<SkillResponse>();
            var skillIDs = skillLevels.Select(m => m.SkillID);
            var levelIDs = skillLevels.Select(m => m.LevelID);
            var memberSkills = _context.Skill.Where(s => skillIDs.Contains(s.ID)).ToList();
            var memberLevels = _context.Levels.Where(l => levelIDs.Contains(l.ID)).ToList();
            foreach (var skillLevel in skillLevels)
            {
                var skillName = memberSkills.Where((s => s.ID == skillLevel.SkillID)).Select(s => s.Name).FirstOrDefault();
                var levelValue = memberLevels.Where((s => s.ID == skillLevel.LevelID)).Select(s => s.Value).FirstOrDefault();
                var memberSkill = new SkillResponse()
                {
                    Name = skillName,
                    Level = levelValue
                };
                response.Add(memberSkill);
            }
            return response;
        }

        public ICollection<SkillLevel>? GetSkillLevelsFromSkillRequest(ICollection<SkillRequest>? skillRequests)
        {
            if (skillRequests == null)
            {
                return null;
            }
            var existingSkills = _context.Skill.Where(s => skillRequests.Select(sr => sr.Name).ToList().Contains(s.Name)).ToList();
            if (existingSkills == null)
            {
                existingSkills = new List<Skill>();
            }
            var skillReqsToBeAdded = skillRequests.Where(sr => !existingSkills.Select(es => es.Name).ToList().Contains(sr.Name)).ToList();

            if (skillReqsToBeAdded != null && skillReqsToBeAdded.Count > 0)
            {
                existingSkills.AddRange(AddSkillsToDb(skillReqsToBeAdded));
            }
            return UpsertSkillLevels(existingSkills, skillRequests.ToList());


        }

        public ICollection<Skill>? AddSkillsToDb(ICollection<SkillRequest>? skillRequests)
        {
            var response = new List<Skill>();

            if (skillRequests == null)
            {
                return null;
            }
            var levels = _context.Levels.Where(x => skillRequests.Select(x => x.Level).Contains(x.Value)).ToList();

            foreach (var request in skillRequests)
            {
                var level = levels.Where(l => l.Value == request.Level).FirstOrDefault();

                response.Add(
                    CreateSkill(request)
                    );
            }
            _context.Skill?.AddRange(response);
            _context.SaveChanges();
            return response;
        }

        public static Skill CreateSkill(SkillRequest skillRequest)
        {
            return new Skill()
            {
                Name = skillRequest.Name
            };
        }

        public Skill GetSkill(string skillName)
        {
            if (skillName == null)
            {
                return null;
            }
            return _context.Skill.Where(s => s.Name == skillName).FirstOrDefault();
        }


        public List<SkillLevel> UpsertSkillLevels(List<Skill> skills, List<SkillRequest> skillRequests)
        {
            var response = new List<SkillLevel>();
            var skillIDs = skills.Select(x => x.ID).ToList();

            var levels = _context.Levels.Where(x => skillRequests.Select(x => x.Level).Contains(x.Value)).ToList();

            var existingSkillLevles = _context.SkillLevels.Where(sl => skillIDs.Contains(sl.SkillID)).ToList();

            var skillLevelsToAdd = new List<SkillLevel>();

            foreach (var request in skillRequests)
            {
                var levelID = levels.Where(l => l.Value == request.Level).Select(l => l.ID).FirstOrDefault();
                if (levelID == null || levelID == Guid.Empty)
                {
                    var userMessage = $"Received level {request.Level} is not allowed";
                    throw new HeistException(userMessage, $"MemberService.UpsertSkillLevel=> {userMessage}");
                }
                var skillID = skills.Where(s => s.Name == request.Name).Select(s => s.ID).FirstOrDefault();
                var skillLevel = existingSkillLevles.Where(esl => esl.SkillID == skillID && esl.LevelID == levelID).FirstOrDefault();
                if (skillLevel == null)
                {
                    skillLevel = new SkillLevel() { LevelID = levelID, SkillID = skillID };
                    skillLevelsToAdd.Add(skillLevel);
                }
                else
                {
                    response.Add(skillLevel);
                }
            }

            _context.SkillLevels.AddRange(skillLevelsToAdd);
            _context.SaveChanges();
            response.AddRange(skillLevelsToAdd);
            return response;
        }

        public List<Skill> GetSkillsFromSkillRequests(List<SkillRequest> request)
        {
            var skillNames = request.Select(s => s.Name).ToList();
            return _context.Skill.Where(s => skillNames.Contains(s.Name)).ToList();
        }

        public List<Level>? GetLevelsFromSkillRequests(List<SkillRequest> request)
        {
            var levelValues = request.Select(s => s.Level).ToList();

            return levelValues == null ? null : _context.Levels.Where(l => levelValues.Contains(l.Value)).ToList();

        }

        public List<HeistSkillLevel> GetHeistSkillLevels(List<HeistSkillRequest> heistSkillRequests)
        {
            var response = new List<HeistSkillLevel>();
            var skillRequests = heistSkillRequests.Select(x => new SkillRequest() { Name = x.Name, Level = x.Level }).ToList();
            var skillLevels = GetSkillLevelsFromSkillRequest(skillRequests);

            var skillsFromRequest = GetSkillsFromSkillRequests(skillRequests);
            var levelsFromRequest = GetLevelsFromSkillRequests(skillRequests);

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
