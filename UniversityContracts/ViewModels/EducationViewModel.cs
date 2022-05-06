using System;
using System.Collections.Generic;
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
        public int UserId { get; set; }

        [DataMember]
        [DisplayName("Название")]
        public string Name { get; set; }

        [DataMember]
        [DisplayName("Количество дисциплин")]
        public int Count { get; set; }

        [DataMember]
        [DisplayName("Стоимость")]
        public decimal Cost { get; set; }

        [DataMember]
        [DisplayName("К оплате")]
        public decimal CostToPay { get; set; }

        [DataMember]
        [DisplayName("Дата обучения")]
        public DateTime EducationDate { get; set; }

        [DataMember]
        public Dictionary<int, string> EducationDisciplines { get; set; }

        [DataMember]
        public Dictionary<int, decimal> CostItemEducation { get; set; }

        [DataMember]
        [DisplayName("Дисциплины")]
        public List<int> DisciplineIds { get; set; }

        [DataMember]
        public List<DisciplineViewModel> Disciplines { get; set; }

        [DataMember]
        public List<CostItemViewModel> CostItems { get; set; }
    }
}
