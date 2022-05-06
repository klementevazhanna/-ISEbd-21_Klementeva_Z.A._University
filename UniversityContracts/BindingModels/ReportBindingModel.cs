using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace UniversityContracts.BindingModels
{
    public class ReportBindingModel
    {
        public string FileName { get; set; }

        public int? UserId { get; set; }

        public string UserEmail { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        [DisplayName("Обучения")]
        public List<int> EducationIds { get; set; }
    }
}
