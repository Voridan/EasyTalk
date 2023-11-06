using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WPF.MVVM.Model
{
    public  class UserModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }

        public UserRole Role { get; set; }

        public byte[]? Photo { get; set; }


    }
}
