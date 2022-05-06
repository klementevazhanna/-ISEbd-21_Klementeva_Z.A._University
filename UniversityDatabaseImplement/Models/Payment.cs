using System.ComponentModel.DataAnnotations;

namespace UniversityDatabaseImplement.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        public decimal Sum { get; set; }

        public int DisciplineId { get; set; }

        public int UserId { get; set; }

        public virtual Discipline Discipline { get; set; }

        public virtual User User { get; set; }
    }
}
