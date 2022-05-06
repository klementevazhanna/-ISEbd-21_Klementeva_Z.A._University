using UniversityContracts.BindingModels;
using UniversityContracts.ViewModels;

namespace UniversityContracts.StoragesContracts
{
    public interface ICostItemStorage
    {
        CostItemViewModel GetElement(CostItemBindingModel model);

        void Insert(CostItemBindingModel model);

        void Update(CostItemBindingModel model);

        void Delete(CostItemBindingModel model);

        int Count();
    }
}
