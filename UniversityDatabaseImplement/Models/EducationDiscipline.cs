namespace UniversityDatabaseImplement.Models
{
    public class EducationDiscipline
    {
        public int Id { get; set; }

        public int DisciplineId { get; set; }

        public int EducationId { get; set; }

        public virtual Discipline Discipline { get; set; }

        public virtual Education Education { get; set; }
    }
}
