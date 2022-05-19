using MoneyHeist2.Entities;
using MoneyHeist2.Helpers;

namespace MoneyHeist2.Data.Repos
{
    public interface IHeistRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveAll();
        //PagedList<Member> GetMembers(MemberParams memberParams);
        //Member GetMember(int id);             
        
    }
}
