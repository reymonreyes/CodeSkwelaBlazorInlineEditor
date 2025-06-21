using System.ComponentModel.DataAnnotations;

namespace TableInlineEditor.Services.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Required()]
        [Display(Name = "Category")]
        public int? CategoryID { get; set; }
        public Category Category { get; set; }
        [Required()]
        [Range(0, 1000000, MaximumIsExclusive = false)]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }
        public bool Discontinued { get; set; }
    }

    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
