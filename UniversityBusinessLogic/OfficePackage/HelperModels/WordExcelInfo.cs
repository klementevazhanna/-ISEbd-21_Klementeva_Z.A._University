using System.Collections.Generic;
using UniversityContracts.ViewModels;

namespace UniversityBusinessLogic.OfficePackage.HelperModels
{
    public class WordExcelInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<EducationViewModel> Educations { get; set; }
    }
}
