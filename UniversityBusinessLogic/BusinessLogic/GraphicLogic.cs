using System;
using System.Collections.Generic;
using System.Linq;
using UniversityContracts.BindingModels;
using UniversityContracts.BusinessLogicContracts;
using UniversityContracts.StoragesContracts;
using UniversityContracts.ViewModels;

namespace UniversityBusinessLogic.BusinessLogic
{
    public class GraphicLogic : IGraphicLogic
    {
        private readonly IEducationStorage _educationStorage;

        public GraphicLogic(IEducationStorage educationStorage)
        {
            _educationStorage = educationStorage;
        }

        public GraphicViewModel GetGraphicByPrice(int userId)
        {
            return new GraphicViewModel
            {
                Title = "Диаграмма стоимости обучений",
                ColumnName = "Обучение",
                ValueName = "Стоимость обучения",
                Data = GetEducation(userId).Select(rec => new Tuple<string, int>(rec.Name, Convert.ToInt32(rec.Cost))).ToList()
            };
        }

        public GraphicViewModel GetGraphicByCount(int userId)
        {
            return new GraphicViewModel
            {
                Title = "Диаграмма количества обучений",
                ColumnName = "Обучение",
                ValueName = "Количество обучений",
                Data = GetEducation(userId).Select(rec => new Tuple<string, int>(rec.Name, rec.Count)).ToList()
            };
        }

        private List<EducationViewModel> GetEducation(int userId)
        {
            return _educationStorage.GetFilteredList(new EducationBindingModel { UserId = userId });
        }
    }
}
