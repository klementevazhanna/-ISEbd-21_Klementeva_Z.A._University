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
        public decimal Price { get; set; }

        [ForeignKey("EducationId")]
        public List<EducationDiscipline> EducationDisciplines { get; set; }

        [ForeignKey("EducationId")]
        public List<Payment> Payment { get; set; }
    }
}
