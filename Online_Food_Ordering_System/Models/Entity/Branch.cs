using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Online_Food_Ordering_System.Models.Entity
{
    public class Branch
    {
        //public Branch()
        //{
        //    this.empCollection = new HashSet<Employee>();
        //}


        [Required(ErrorMessage ="Branch ID is required")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int BID { get; set; }

        [Display(Name = "Branch Name")]
        [Required(ErrorMessage ="Branch name is required")]
        public string brnName { get; set; }


        [Display(Name = "Branch Address")]
        [Required(ErrorMessage ="Branch address is required")]
        public string brnAddress { get; set; }

        //[ForeignKey("empID")]
        //public virtual ICollection<Employee> empCollection { get; set; }
    }
}