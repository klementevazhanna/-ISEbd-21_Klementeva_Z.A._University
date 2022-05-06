namespace UniversityContracts.BindingModels
{
    public class PaymentBindingModel
    {
        public int? Id { get; set; }

        public int DisciplineId { get; set; }

        public int UserId { get; set; }

        public decimal? Sum { get; set; }
    }
}
