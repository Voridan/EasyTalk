using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public enum UserRole
    {
        [Description("admin")]
        Admin,

        [Description("dev")]
        Developer,

        [Description("client")]
        Client
    }
}
