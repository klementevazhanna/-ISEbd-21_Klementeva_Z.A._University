using System;
using System.Collections.Generic;
using UniversityContracts.BindingModels;
using UniversityContracts.BusinessLogicContracts;
using UniversityContracts.StoragesContracts;
using UniversityContracts.ViewModels;

namespace UniversityBusinessLogic.BusinessLogic
{
    public class DisciplineLogic : IDisciplineLogic
    {
        private readonly IDisciplineStorage _disciplineStorage;

        public DisciplineLogic(IDisciplineStorage disciplineStorage)
        {
            _disciplineStorage = disciplineStorage;
        }

        public List<DisciplineViewModel> Read(DisciplineBindingModel model)
        {
            if (model == null)
            {
                return _disciplineStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<DisciplineViewModel> { _disciplineStorage.GetElement(model) };
            }
            return _disciplineStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(DisciplineBindingModel model)
        {
            var discipline = _disciplineStorage.GetElement(new DisciplineBindingModel
            {
                Name = model.Name
            });
            if (discipline != null && discipline.Id != model.Id)
            {
                throw new Exception("Уже есть дисциплина с таким названием");
            }
            if (model.Id.HasValue)
            {
                _disciplineStorage.Update(model);
            }
            else
            {
                _disciplineStorage.Insert(model);
            }
        }

        public void Delete(DisciplineBindingModel model)
        {
            var discipline = _disciplineStorage.GetElement(new DisciplineBindingModel
            {
                Id = model.Id
            });
            if (discipline == null)
            {
                throw new Exception("Дисциплина не найдена");
            }
            _disciplineStorage.Delete(model);
        }
    }
}
