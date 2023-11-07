using DAL.Models;

namespace BLL.Models
{
    public class UserModel : LoginUserModel
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public UserRole Role { get; set; }

        public byte[]? Photo { get; set; }

        public UserModel(string nickname, string firstName, string lastName, string email, string password, UserRole role, byte[]? photo) : base(nickname, password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Photo = photo;
            Role = role;
        }
    }
}
