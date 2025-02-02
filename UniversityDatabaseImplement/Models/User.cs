﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDatabaseImplement.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string FIO { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [ForeignKey("UserId")]
        public virtual List<Education> Educations { get; set; }

        [ForeignKey("UserId")]
        public virtual List<Payment> Payments { get; set; }
    }
}
