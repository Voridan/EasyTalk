using DAL.Models;

namespace BLL.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        
        public string NickName { get; set; }

        public string Password { get; set; }

        public string? Email { get; set; }

        public UserRole Role { get; set; }

        public byte[]? Photo { get; set; }

        public UserModel()
        {
        }

        public UserModel(string nickname, string firstName, string lastName, string email, string password, UserRole role = UserRole.User , byte[]? photo=null)
        {
            FirstName = firstName;
            LastName = lastName;
            NickName = nickname;
            Password = password;
            Email = email;
            Photo = photo;
            Role = role;
        }

    }
}
