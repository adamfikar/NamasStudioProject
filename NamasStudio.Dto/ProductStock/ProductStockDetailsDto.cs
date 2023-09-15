using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Dto.ProductStock
{
    public class ProductStockDetailsDto
    {
        public int ProductId { get; set; }

        //[Range(1, int.MaxValue, ErrorMessage = "Please fill Size")]
        //[UniqueSizeProduct("ProductId", "SizeId")]
        public int SizeId { get; set; }

        public string SizeName { get; set; }

        //[Range(1, int.MaxValue, ErrorMessage = "Please fill Stock")]
        public int Stock { get; set; }
    }
}
