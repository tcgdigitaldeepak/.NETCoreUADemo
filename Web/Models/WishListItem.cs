using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
   
    public class WishlistItem
    {
        public string id { get; set; }
        public string country { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        // public string type => typeof(WishlistItem).Name;
    }
}
