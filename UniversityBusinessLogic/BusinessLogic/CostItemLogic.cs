using System;
using UniversityContracts.BindingModels;
using UniversityContracts.BusinessLogicContracts;
using UniversityContracts.StoragesContracts;
using UniversityContracts.ViewModels;

namespace UniversityBusinessLogic.BusinessLogic
{
    public class CostItemLogic : ICostItemLogic
    {
        private readonly ICostItemStorage _costItemStorage;

        public CostItemLogic(ICostItemStorage routeStorage)
        {
            _costItemStorage = routeStorage;
        }

        public CostItemViewModel GetElement(CostItemBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return _costItemStorage.GetElement(model);
        }

        public void CreateOrUpdate(CostItemBindingModel model)
        {
            var costItem = _costItemStorage.GetElement(new CostItemBindingModel
            {
                Name = model.Name
            });
            if (costItem != null && costItem.Id != model.Id)
            {
                throw new Exception("Уже есть элемент с таким названием");
            }
            if (model.Id.HasValue)
            {
                _costItemStorage.Update(model);
            }
            else
            {
                _costItemStorage.Insert(model);
            }
        }

        public void Delete(CostItemBindingModel model)
        {
            var costItem = _costItemStorage.GetElement(new CostItemBindingModel
            {
                Id = model.Id
            });
            if (costItem == null)
            {
                throw new Exception("Не найдено");
            }
            _costItemStorage.Delete(model);
        }

        public int Count()
        {
            return _costItemStorage.Count();
        }
    }
}
