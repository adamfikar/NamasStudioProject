﻿using NamasStudio.Dto.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Dto.Auth
{
    public class RegisterDto
    {
        [UniqueUsername]
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [UniqueEmail]
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string? LastName { get; set; }
        public string? Gender { get; set; } 
        public DateTime? BirthDate { get; set; }
    }
}
