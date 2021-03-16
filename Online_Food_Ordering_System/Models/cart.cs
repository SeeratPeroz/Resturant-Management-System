using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Food_Ordering_System.Models
{
    public class cart
    {
        public int prdID { get; set; }
        public string prdName { get; set; }
        public int price { get; set; }
        public int qty { get; set; }
        public int bill { get; set; }
    }
}