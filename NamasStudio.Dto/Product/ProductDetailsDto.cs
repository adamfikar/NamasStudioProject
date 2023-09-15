using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Dto.Product
{
    public class ProductDetailsDto
    {

        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        public string ProductName { get; set; } = null!;

        public decimal UnitPrice { get; set; }

        public string? Description { get; set; }

        public decimal Weight { get; set; }

        public string Fabric { get; set; } = null!;

        public string Color { get; set; } = null!;
    }
}
