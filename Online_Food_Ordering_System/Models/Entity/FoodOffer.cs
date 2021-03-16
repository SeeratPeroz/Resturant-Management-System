using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Online_Food_Ordering_System.Models.Entity
{
    public class FoodOffer
    {
        [Key]
        public int fofID { get; set; }


        [Required (ErrorMessage = "Food ID is required")]
        [Display (Name= "Food ID")]
        public int prdID { get; set; }
        public virtual Product prdClass { get; set; }



        //[Required(ErrorMessage = "Food Offer Price is required")]
        [Display(Name = "Food Price Offer")]
        public int fofPrice { get; set; }


        [Display(Name = "Food Offer Description ")]
        public string fofDesc { get; set; }


    }
}