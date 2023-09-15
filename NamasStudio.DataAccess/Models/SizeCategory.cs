using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.DataAccess.Models
{
    public class SizeCategory
    {
        public int SizeId { get; set; }
        public int CategoryId { get; set; }
        public virtual ProductSize ProductSize { get; set; } = null!;
        public virtual CategoryProduct CategoryProduct { get; set; } = null!;
    }
}
