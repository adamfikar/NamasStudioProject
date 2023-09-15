using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Dto.ProductPhoto
{
    public class ProductPhotoGridDto
    {
        public int PhotoId { get; set; }

        public int ProductId { get; set; }

        public string PathName { get; set; } = null!;
    }
}
