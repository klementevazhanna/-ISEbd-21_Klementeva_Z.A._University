using System.ComponentModel;
using System.Runtime.Serialization;

namespace UniversityContracts.ViewModels
{
    [DataContract]
    public class EducationViewModel
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
        [DisplayName("Осталось заплатить")]
        public decimal PriceToPay { get; set; }
    }
}
