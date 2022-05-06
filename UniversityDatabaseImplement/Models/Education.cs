using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDatabaseImplement.Models
{
    public class Education
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [Required]
        public DateTime EducationDate { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        [ForeignKey("EducationId")]
        public virtual List<EducationDiscipline> EducationsDisciplines { get; set; }

        [ForeignKey("EducationId")]
        public virtual List<CostItemEducation> CostItemsEducations { get; set; }
    }
}
