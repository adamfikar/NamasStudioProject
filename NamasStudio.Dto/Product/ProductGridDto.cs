using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Dto.Product
{
    public class ProductGridDto
    {
        public int ProductId { get; set; }

        public string CategoryName { get; set; }

        public string ProductName { get; set; } = null!;

        public string UnitPrice { get; set; }

        public string? Description { get; set; }

        public decimal Weight { get; set; }

        public string Fabric { get; set; } = null!;

        public string Color { get; set; } = null!;
    }
}
