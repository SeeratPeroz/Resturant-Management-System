using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Online_Food_Ordering_System.Models.Entity
{
    public class Customer
    {
        //public Customer()
        //{
        //    this.ordCollection = new HashSet<Order>();
        //}


        [Required(ErrorMessage ="Customer ID is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int custID { get; set; }
        [Required(ErrorMessage ="Customer name is required.")]
        [Display (Name = "Customer Name")]
        public string custName { get; set; }

        [Display(Name = "Customer Phone")]
        public string custPhone { get; set; }

        [Display(Name = "Customer Address")]
        [Required(ErrorMessage ="Customer address is required.")]
        public string custAddress { get; set; }

        [Display(Name = "Customer Email")]
        [Required (ErrorMessage ="Email is required.")]
        public string custEmail { get; set; }

        [Display(Name = "Customer Password")]
        [Required (ErrorMessage ="Password is Required.")]
        public string custPassword { get; set; }


    
       // public ICollection<Order> ordCollection { get; set; }
    }
}