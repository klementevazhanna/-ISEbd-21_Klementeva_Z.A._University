using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UniversityContracts.BindingModels
{
    [DataContract]
    public class EducationBindingModel
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public int? UserId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public decimal Cost { get; set; }

        [DataMember]
        public DateTime DateVisit { get; set; }

        [DataMember]
        public DateTime? DateFrom { get; set; }

        [DataMember]
        public DateTime? DateTo { get; set; }

        [DataMember]
        public Dictionary<int, string> Disciplines { get; set; }

        [DataMember]
        public List<int> PickedEducations { get; set; }

        public bool? NoCost { get; set; }
    }
}
