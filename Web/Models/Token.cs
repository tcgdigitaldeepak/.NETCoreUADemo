using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    [Table("tbl_Token")]
    public class Token
    {
        [Key]
        public string UserId { get; set; }
        public string token { get; set; }
      
    }
}
