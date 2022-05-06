namespace UniversityDatabaseImplement.Models
{
    public class CostItemEducation
    {
        public int Id { get; set; }

        public int CostItemId { get; set; }

        public int EducationId { get; set; }

        public virtual CostItem CostItem { get; set; }

        public virtual Education Education { get; set; }
    }
}
