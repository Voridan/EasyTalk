using DAL.Models;

namespace BLL.Models
{
    public class UserModel
    {
        public Guid Id { get; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        
        public string NickName { get; set; }

        public string Password { get; set; }

        public string? Email { get; set; }

        public UserRole Role { get; set; }

        public byte[]? Photo { get; set; }

        public UserModel(string nickname, string firstName, string lastName, string email, string password, UserRole role, byte[]? photo)
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
