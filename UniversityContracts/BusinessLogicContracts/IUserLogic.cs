using System.Collections.Generic;
using UniversityContracts.BindingModels;
using UniversityContracts.ViewModels;

namespace UniversityContracts.BusinessLogicContracts
{
    public interface IUserLogic
    {
        List<UserViewModel> Read(UserBindingModel model);

        void CreateOrUpdate(UserBindingModel model);

        void Delete(UserBindingModel model);
    }
}
