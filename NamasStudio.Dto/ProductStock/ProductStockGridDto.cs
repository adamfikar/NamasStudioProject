using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Dto.ProductStock
{
    public class ProductStockGridDto
    {
        public int ProductId { get; set; }

        public string SizeName { get; set; }

        public int SizeId { get; set; }

        public int Stock { get; set; }
    }
}
