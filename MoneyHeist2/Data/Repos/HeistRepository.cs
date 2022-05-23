using MoneyHeist2.Entities;
using MoneyHeist2.Helpers;

namespace MoneyHeist2.Data.Repos
{
    public class HeistRepository : IHeistRepository
    {
        private readonly DataContext _context;
        public HeistRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public Member GetMember(Guid id)
        {
            var member= _context.Members.FirstOrDefault(x=>x.ID==id);
            if (member==null)
            {
                throw new Exception("Member not found");
            }
            else { return member; }
        }

        public Member GetMember(int id)
        {
            throw new NotImplementedException();
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
