using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class LoginDto
    {
        [Key]
        public string Email { get; set; }

        public string Password { get; set; }

        public string token { get; set; }
    }

    public class LoginResponse
    {
        public string token { get; set; }
    }
}
