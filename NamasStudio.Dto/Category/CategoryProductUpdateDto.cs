using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Dto.Category
{
    public class CategoryProductUpdateDto
    {
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public DateTime? UpdateAt { get; } = DateTime.Now;
    }

}
