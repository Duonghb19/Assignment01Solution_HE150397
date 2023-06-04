using BusinessObject.Models;

namespace DataAccess
{
    public class MemberDAO
    {
        public static Member Login(string username, string password)
        {
            var member = new Member();
            try
            {
                using (var connection = new PRN231_AS1Context())
                {
                    member = connection.Members.FirstOrDefault(x => x.Email.Equals(username) && x.Password.Equals(password));
                }
            }
            catch (Exception)
            {

                throw;
            }
            return member;
        }
        public static List<Member> GetMember()
        {
            var listMem = new List<Member>();
            try
            {
                using (var connection = new PRN231_AS1Context())
                {
                    listMem = connection.Members.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return listMem;
        }
        public static Member GetMemberById(int id)
        {
            var member = new Member();
            try
            {
                using (var connection = new PRN231_AS1Context())
                {
                    member = connection.Members.SingleOrDefault(x => x.MemberId == id);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return member;
        }
        public static void SaveMember(Member mem)
        {
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    context.Members.Add(mem);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public static void UpdateMember(Member mem)
        {
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    context.Entry<Member>(mem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public static void DeleteMember(Member mem)
        {
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    var member = context.Members.SingleOrDefault(c => c.MemberId == mem.MemberId);
                    context.Members.Remove(member);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
