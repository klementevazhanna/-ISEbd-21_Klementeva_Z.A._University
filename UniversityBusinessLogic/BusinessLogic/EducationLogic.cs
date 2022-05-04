using System;
using System.Collections.Generic;
using UniversityContracts.BindingModels;
using UniversityContracts.BusinessLogicContracts;
using UniversityContracts.StoragesContracts;
using UniversityContracts.ViewModels;

namespace UniversityBusinessLogic.BusinessLogic
{
    public class EducationLogic : IEducationLogic
    {
        private readonly IEducationStorage _educationStorage;

        public EducationLogic(IEducationStorage educationStorage)
        {
            _educationStorage = educationStorage;
        }

        public List<EducationViewModel> Read(EducationBindingModel model)
        {
            if (model == null)
            {
                return _educationStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<EducationViewModel> { _educationStorage.GetElement(model) };
            }
            return _educationStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(EducationBindingModel model)
        {
            var education = _educationStorage.GetElement(new EducationBindingModel
            {
                Name = model.Name
            });
            if (education != null && education.Id != model.Id)
            {
                throw new Exception("Уже есть обучение с таким названием");
            }
            if (model.Id.HasValue)
            {
                _educationStorage.Update(model);
            }
            else
            {
                _educationStorage.Insert(model);
            }
        }

        public void Delete(EducationBindingModel model)
        {
            var element = _educationStorage.GetElement(new EducationBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Обучение не найдено");
            }
            _educationStorage.Delete(model);
        }
    }
}
