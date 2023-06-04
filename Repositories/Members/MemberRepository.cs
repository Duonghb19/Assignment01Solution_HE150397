using BusinessObject.Models;
using DataAccess;

namespace Repositories.Members
{
    public class MemberRepository : IMemberRepository
    {
        public void DeleteMember(Member member) => MemberDAO.DeleteMember(member);

        public Member GetMemberById(int id) => MemberDAO.GetMemberById(id);

        public List<Member> GetMembers() => MemberDAO.GetMember();

        public List<Order> GetOrders() => OrderDAO.GetOrder();

        public Member Login(string username, string password) => MemberDAO.Login(username, password);

        public void SaveMember(Member member) => MemberDAO.SaveMember(member);

        public void UpdateMember(Member member) => MemberDAO.UpdateMember(member);
    }
}
