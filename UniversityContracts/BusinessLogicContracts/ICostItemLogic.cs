using UniversityContracts.BindingModels;
using UniversityContracts.ViewModels;

namespace UniversityContracts.BusinessLogicContracts
{
    public interface ICostItemLogic
    {
        CostItemViewModel GetElement(CostItemBindingModel model);

        void CreateOrUpdate(CostItemBindingModel model);

        void Delete(CostItemBindingModel model);

        public int Count();
    }
}
