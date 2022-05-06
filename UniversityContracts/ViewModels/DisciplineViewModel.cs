using System.ComponentModel;
using System.Runtime.Serialization;

namespace UniversityContracts.ViewModels
{
    [DataContract]
    public class DisciplineViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("Название")]
        public string Name { get; set; }

        [DataMember]
        [DisplayName("Цена")]
        public decimal Price { get; set; }

        [DataMember]
        [DisplayName("К оплате")]
        public decimal PriceToPay { get; set; }
    }
}
