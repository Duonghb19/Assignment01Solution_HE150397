using BusinessObject.Models;

namespace Repositories.Members
{
    public interface IMemberRepository
    {
        List<Member> GetMembers();
        void SaveMember(Member member);
        Member GetMemberById(int id);
        void DeleteMember(Member member);
        void UpdateMember(Member p);
        List<Order> GetOrders();
        Member Login(string username, string password);
    }
}
