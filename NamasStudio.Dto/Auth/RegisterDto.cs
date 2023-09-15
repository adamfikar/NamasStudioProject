using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Dto.Auth
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string? LastName { get; set; }

        public string Gender { get; set; } = null!;

        public DateTime? BirthDate { get; set; }
    }
}
