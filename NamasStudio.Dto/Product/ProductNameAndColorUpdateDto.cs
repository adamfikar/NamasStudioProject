using NamasStudio.Dto.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Dto.Product
{
    public class ProductNameAndColorUpdateDto
    {
        [Required(ErrorMessage = "Please fill Product Name")]
        [UniqueProductNameAndColor("ProductName", "Color", ErrorMessage = "Product with the same name and color already exists.")]
        public string ProductName { get; set; } = null!;

        [Required(ErrorMessage = "Please fill Color")]
        [UniqueProductNameAndColor("ProductName", "Color", ErrorMessage = "Product with the same name and color already exists.")]
        public string Color { get; set; } = null!;
    }
}
