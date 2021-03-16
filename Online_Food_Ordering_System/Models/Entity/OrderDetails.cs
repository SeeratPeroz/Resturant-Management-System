using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Online_Food_Ordering_System.Models.Entity
{
    public class OrderDetails
    {
        [Key]
        public int ID { get; set; }



        // Order - OrderDetails 1:M
      //  [ForeignKey("ordID")]
        [Required(ErrorMessage ="Order ID is required")]
        public int ordID { get; set; }
        public virtual Order ordClass { get; set; }


        // Product - OrderDetails 1:M
      //  [ForeignKey("prdID")]
        [Required(ErrorMessage="Food ID is required")]
        [Display (Name = "Food ID")]
        public int prdID { get; set; }
        public virtual Product prdClass { get; set; }



        [Required(ErrorMessage ="Price is required")]
        public int Price { get; set; }


        [Required(ErrorMessage ="Quantity is required")]
        public int Qty { get; set; }




    }
}