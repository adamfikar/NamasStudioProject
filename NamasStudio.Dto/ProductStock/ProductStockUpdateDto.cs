using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Dto.ProductStock
{
    public class ProductStockUpdateDto
    {
        public int ProductId { get; set; }

        //[Range(1, int.MaxValue, ErrorMessage = "Please fill Size")]
        //[UniqueSizeProduct("ProductId", "SizeId")]
        public int SizeId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please fill stock more than/equal zero")]
        public int Stock { get; set; }
    }
}
