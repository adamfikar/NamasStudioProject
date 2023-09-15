using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Services.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException() : base($"Oops! Forbidden Endpoint! ")
        {

        }
    }
}
