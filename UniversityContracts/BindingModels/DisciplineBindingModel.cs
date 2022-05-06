namespace UniversityContracts.BindingModels
{
    public class DisciplineBindingModel
    {
        public int? Id { get; set; }

        public int? EducationId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
