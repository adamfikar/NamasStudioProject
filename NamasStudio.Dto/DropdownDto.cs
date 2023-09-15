using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Dto
{
    public class DropdownDto
    {
        public object Value { get; set; }
        public string Text { get; set; }

        public DropdownDto()
        {
        }

        public DropdownDto(object value, string text)
        {
            this.Value = value;
            this.Text = text;
        }
    }
}
