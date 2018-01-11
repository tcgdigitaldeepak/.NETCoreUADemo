using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CouchBaseCore.Models
{
   
    public class Token
    {
        //[Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string token { get; set; }

    }
}
