using System.Collections.Generic;
using System.Linq;
using UniversityBusinessLogic.OfficePackage;
using UniversityBusinessLogic.OfficePackage.HelperModels;
using UniversityContracts.BindingModels;
using UniversityContracts.BusinessLogicContracts;
using UniversityContracts.StoragesContracts;
using UniversityContracts.ViewModels;

namespace UniversityBusinessLogic.BusinessLogic
{
    public class ReportLogic : IReportLogic
    {
        private readonly IEducationStorage _educationStorage;

        private readonly AbstractSaveToExcel _saveToExcel;

        private readonly AbstractSaveToWord _saveToWord;

        private readonly AbstractSaveToPdf _saveToPdf;

        public ReportLogic(IEducationStorage educationStorage,
            AbstractSaveToExcel saveToExcel, AbstractSaveToWord saveToWord, AbstractSaveToPdf saveToPdf)
        {
            _educationStorage = educationStorage;

            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
        }

        public List<EducationViewModel> GetEducations(ReportBindingModel model)
        {
            return _educationStorage.GetFilteredByDateList(new EducationBindingModel
            {
                UserId = model.UserId,
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            }).ToList();
        }

        public void SaveEducationsToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDoc(new WordExcelInfo
            {
                FileName = model.FileName,
                Title = "Список обучений",
                Educations = _educationStorage.GetFilteredByPickList(new EducationBindingModel
                {
                    PickedEducations = model.EducationIds
                })
            });
        }

        public void SaveEducationsToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateReport(new WordExcelInfo
            {
                FileName = model.FileName,
                Title = "Список обучений",
                Educations = _educationStorage.GetFilteredByPickList(new EducationBindingModel
                {
                    PickedEducations = model.EducationIds
                })
            });
        }

        public void SaveEducationsToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список обучений",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Educations = GetEducations(model)
            });
        }
    }
}
