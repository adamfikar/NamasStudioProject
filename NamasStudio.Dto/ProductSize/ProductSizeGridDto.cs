using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Dto.ProductSize
{
    public class ProductSizeGridDto
    {
        public int SizeId { get; set; }
        public int CategoryId { get; set; }
        public string SizeName { get; set; } 
        public string CategoryName { get; set; }

        public string? Waist { get; set; }

        public string? Hips { get; set; }

        public string? LengthLower { get; set; }

        public string? Bust { get; set; }

        public string? LengthUpper { get; set; }

        public string? ArmHole { get; set; }

        public string? BottomSleeve { get; set; }

        public string? SleeveLength { get; set; }

        public string? Description { get; set; }
    }
}
