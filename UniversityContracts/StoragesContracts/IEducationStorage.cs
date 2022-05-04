using System.Collections.Generic;
using UniversityContracts.BindingModels;
using UniversityContracts.ViewModels;

namespace UniversityContracts.StoragesContracts
{
    public interface IEducationStorage
    {
        List<EducationViewModel> GetFullList();

        List<EducationViewModel> GetFilteredList(EducationBindingModel model);

        EducationViewModel GetElement(EducationBindingModel model);

        void Insert(EducationBindingModel model);

        void Update(EducationBindingModel model);

        void Delete(EducationBindingModel model);
    }
}
