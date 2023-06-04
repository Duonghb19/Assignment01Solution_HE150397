namespace eStoreAPI.DTO.Requests.Members
{
    public class UserModel
    {
        public int accountID { get; set; }
        public int roleID { get; set; }
        public string username { get; set; }
        public string u_password { get; set; }
        public string fullname { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string a_address { get; set; }
    }
}
