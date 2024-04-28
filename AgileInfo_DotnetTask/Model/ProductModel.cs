using System.ComponentModel.DataAnnotations;

namespace AgileInfoTask.Model
{
    public class ProductModel
    {
        [Key]
        public int ProductID { get; set; }

        public string? ProductName { get; set; }

        public int ProductPrice { get; set; }

        public int ProductQuantity { get; set; }
    }
}
