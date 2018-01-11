using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jWtAuthConsumer
{
    public class LoginDto
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string token { get; set; }
    }

    public class LoginResponse
    {
        public string token { get; set; }
    }
}
