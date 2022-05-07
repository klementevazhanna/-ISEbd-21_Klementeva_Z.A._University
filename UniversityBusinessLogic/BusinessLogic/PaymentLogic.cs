using System;
using System.Collections.Generic;
using UniversityContracts.BindingModels;
using UniversityContracts.BusinessLogicContracts;
using UniversityContracts.StoragesContracts;
using UniversityContracts.ViewModels;

namespace UniversityBusinessLogic.BusinessLogic
{
    public class PaymentLogic : IPaymentLogic
    {
        private readonly IPaymentStorage _paymentStorage;

        public PaymentLogic(IPaymentStorage paymentStorage)
        {
            _paymentStorage = paymentStorage;
        }

        public List<PaymentViewModel> Read(PaymentBindingModel model)
        {
            if (model == null)
            {
                return _paymentStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<PaymentViewModel> { _paymentStorage.GetElement(model) };
            }
            return _paymentStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(PaymentBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _paymentStorage.Update(model);
            }
            else
            {
                _paymentStorage.Insert(model);
            }
        }

        public void Delete(PaymentBindingModel model)
        {
            var payment = _paymentStorage.GetElement(new PaymentBindingModel
            {
                Id = model.Id
            });
            if (payment == null)
            {
                throw new Exception("Платёж не найден");
            }
            _paymentStorage.Delete(model);
        }
    }
}
