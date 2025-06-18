using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.DTOs.Resquest
{
    public class LoginRequest
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
