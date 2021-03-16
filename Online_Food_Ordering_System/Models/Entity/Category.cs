using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Online_Food_Ordering_System.Models.Entity
{
    public class Category
    {
        public Category()
        {
            this.prdCollection = new HashSet<Product>();
        }

        [Display (Name = "Category ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int catID { get; set; }

        [Display (Name ="Category Name")]
        [Required(ErrorMessage = "Category name is required")]
        public string catName { get; set; }

        [Display(Name ="Category Descriptions")]
        public string catDesc { get; set; }

        [Display (Name = "Min Price in Category")]
        public int catMinPrice { get; set; }

        [Display (Name = "Views in Category")]
        public int catViews { get; set; }

        [Display (Name = "Delivery Time")]
        public int catDelivery { get; set; }

        [Display (Name = "Category Image")]
        public string ImagePath { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public virtual ICollection<Product> prdCollection { get; set; }

    }
}