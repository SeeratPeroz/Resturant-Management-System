using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Online_Food_Ordering_System.Models.Entity
{
    public class Employee
    {
        public Employee()
        {
            this.brnCollection = new HashSet<Branch>();
        }

        [Required(ErrorMessage ="Employee ID is required")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int empID { get; set; }



        [Required(ErrorMessage ="Emloyee name is required")]
        public string empName { get; set; }


        public string empPhone { get; set; }



        [Required(ErrorMessage ="Employee address is required")]
        public string empAddress { get; set; }



        [Required(ErrorMessage = "Email is required.")]
        public string empEmail { get; set; }


        [Required(ErrorMessage = "Password is required.")]
        public string empPassword { get; set; }



        [Required (ErrorMessage ="Employee role is required.")]
        public string empRole { get; set; }



        // Branch Employee Model M:M
        public virtual ICollection<Branch> brnCollection { get; set; }


        // SalesOrder - Employee  1:M
    }
}