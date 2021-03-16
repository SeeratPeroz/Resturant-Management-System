using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Online_Food_Ordering_System.Models.Entity
{
    public class Order
    {
        public Order()
        {
            this.OrdDetailsCollection = new HashSet<OrderDetails>();
            paid = false;
        }


        [Required(ErrorMessage ="Order ID is required")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ordID { get; set; }


        [Required(ErrorMessage ="Order Date is required")]
        public DateTime ordDate { get; set; }



        [Display (Name = "Order Location")]

        [Required(ErrorMessage ="Order Location is required")]
        public string ordLocation { get; set; }


        [Display (Name = "Total Bill")]
        public int bill { get; set; }


        // Order - Customer 1:M
        [Required(ErrorMessage ="Customer ID is required")]


      //  [ForeignKey("custID")]
      [Display (Name = "Customer Name")]
        public string custName { get; set; }
        //public virtual Customer custClass { get; set; }

        public bool paid { get; set; }

        [Display (Name = "Employee Name")]
        public string empName { get; set; }


        // Order - OrderDetails 1:M
        public ICollection<OrderDetails> OrdDetailsCollection { get; set; }


     

    }
}