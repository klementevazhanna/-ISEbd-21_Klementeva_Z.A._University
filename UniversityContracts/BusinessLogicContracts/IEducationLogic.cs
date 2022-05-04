using System.Collections.Generic;
using UniversityContracts.BindingModels;
using UniversityContracts.ViewModels;

namespace UniversityContracts.BusinessLogicContracts
{
    public interface IEducationLogic
    {
        List<EducationViewModel> Read(EducationBindingModel model);

        void CreateOrUpdate(EducationBindingModel model);

        void Delete(EducationBindingModel model);
    }
}
