using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Online_Food_Ordering_System.Models.Entity
{
    public class Product
    {
        public Product()
        {
            this.ordDetaiCollection = new HashSet<OrderDetails>();
        }


        [Required(ErrorMessage ="Prodact ID is required")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int prdID { get; set; }

        [Required(ErrorMessage ="Product name is required")]
        [Display (Name = "Product Name")]
        public string prdName { get; set; }

        [Display (Name = "Product Price")]
        [Required(ErrorMessage ="Price is required")]
        public int prdPrice { get; set; }



        [Display(Name = "Product Image")]
        public string ImagePath { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }



        [Display(Name = "Product Description")]
        [Required(ErrorMessage ="Product Description is required")]
        public string prdDescription { get; set; }
        [Required(ErrorMessage ="Category ID is required ")]


        // [ForeignKey("catID")]
        [Display(Name = "Category Name")]
        public int catID { get; set; }
        public virtual Category catClass { get; set; }


        public virtual ICollection<OrderDetails> ordDetaiCollection { get; set; }

        public virtual ICollection<FoodOffer> fofCollection { get; set; }

    }
}