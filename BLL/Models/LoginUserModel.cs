namespace BLL.Models
{
    public class LoginUserModel
    {
        public string NickName { get; set; }
        
        public string Password { get; set; }

        public LoginUserModel(string nickname, string password)
        {
            NickName = nickname;
            Password = password;
        }
    }
}
