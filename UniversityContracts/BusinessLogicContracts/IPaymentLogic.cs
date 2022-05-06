using System.Collections.Generic;
using UniversityContracts.BindingModels;
using UniversityContracts.ViewModels;

namespace UniversityContracts.BusinessLogicContracts
{
    public interface IPaymentLogic
    {
        List<PaymentViewModel> Read(PaymentBindingModel model);

        void CreateOrUpdate(PaymentBindingModel model);

        void Delete(PaymentBindingModel model);
    }
}
