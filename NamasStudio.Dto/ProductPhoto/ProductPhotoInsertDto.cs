using NamasStudio.Dto.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Dto.ProductPhoto
{
    public class ProductPhotoInsertDto
    {
        public int ProductId { get; set; }

        [Required]
        [CheckTypeFile("PathName")]
        public string PathName { get; set; }
    }
}
