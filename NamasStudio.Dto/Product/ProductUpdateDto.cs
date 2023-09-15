using NamasStudio.Dto.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Dto.Product
{
    public class ProductUpdateDto
    {
        public int ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please fill Category")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please fill Product Name")]
        public string ProductName { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "Please fill Price")]
        public decimal UnitPrice { get; set; }

        public string? Description { get; set; }

        [Range(0.1, int.MaxValue, ErrorMessage = "Please fill Weight")]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "Please fill Fabric")]
        public string Fabric { get; set; } = null!;

        [Required(ErrorMessage = "Please fill Color")]
        public string Color { get; set; } = null!;

        [UniqueProductNameAndColor("ProductName", "Color", ErrorMessage = "Product with the same name and color already exists.")]
        public bool IsUnique { get; set; } // This property is just for validation purposes, not stored in the database

    }
}
