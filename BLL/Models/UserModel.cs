using DAL.Models;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;

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

        public UserModel(Guid id, string nickname, string firstName, string lastName, string email, string password, UserRole role = UserRole.User, byte[]? photo=null)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            NickName = nickname;
            Password = password;
            Email = email;
            Photo = photo;
            Role = role;
        }

        public static string Serialize(UserModel user)
        {
            return JsonConvert.SerializeObject(user);
        }

        public static UserModel? Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<UserModel>(json);
        }
    }
}
