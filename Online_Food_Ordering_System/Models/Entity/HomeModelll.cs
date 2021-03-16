using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Food_Ordering_System.Models.Entity
{
    public class HomeModelll
    {
        public IEnumerable<Category> catModel { get; set; }
        public IEnumerable<FoodOffer> offerModel { get; set; }
    }
}