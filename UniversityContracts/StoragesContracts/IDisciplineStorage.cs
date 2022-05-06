using System.Collections.Generic;
using UniversityContracts.BindingModels;
using UniversityContracts.ViewModels;

namespace UniversityContracts.StoragesContracts
{
    public interface IDisciplineStorage
    {
        List<DisciplineViewModel> GetFullList();

        List<DisciplineViewModel> GetFilteredList(DisciplineBindingModel model);

        DisciplineViewModel GetElement(DisciplineBindingModel model);

        void Insert(DisciplineBindingModel model);

        void Update(DisciplineBindingModel model);

        void Delete(DisciplineBindingModel model);
    }
}
