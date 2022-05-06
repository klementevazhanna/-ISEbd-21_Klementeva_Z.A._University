using System;
using System.ComponentModel;

namespace UniversityContracts.ViewModels
{
    public class PaymentViewModel
    {
        public int Id { get; set; }

        public int DisciplineId { get; set; }

        public int UserId { get; set; }

        [DisplayName("Оплачено")]
        public decimal? Sum { get; set; }

        [DisplayName("Дата оплаты")]
        public DateTime? PaymentDate { get; set; }
    }
}
