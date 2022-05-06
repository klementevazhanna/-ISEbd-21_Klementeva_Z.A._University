using System.Collections.Generic;
using UniversityContracts.BindingModels;
using UniversityContracts.ViewModels;

namespace UniversityContracts.BusinessLogicContracts
{
    public interface IReportLogic
    {
        public List<EducationViewModel> GetEducations(ReportBindingModel model);

        public void SaveEducationsToWordFile(ReportBindingModel model);

        public void SaveEducationsToExcelFile(ReportBindingModel model);

        public void SaveEducationsToPdfFile(ReportBindingModel model);
    }
}
