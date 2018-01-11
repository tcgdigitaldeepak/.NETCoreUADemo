using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class Ticket
    {
       
            public int ID { get; set; }
            public string Title { get; set; }
            public DateTime Date { get; set; }
            public decimal Price { get; set; }
        
    }
}
