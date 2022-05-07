using Microsoft.AspNetCore.Mvc;
using UniversityContracts.BindingModels;
using UniversityContracts.BusinessLogicContracts;

namespace UniversityRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IPaymentLogic _logic;

        public PaymentController(IPaymentLogic logic)
        {
            _logic = logic;
        }

        [HttpPost]
        public void Pay(PaymentBindingModel model) => _logic.CreateOrUpdate(model);
    }
}
