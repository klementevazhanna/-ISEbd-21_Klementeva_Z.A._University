using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using UniversityContracts.BindingModels;
using UniversityContracts.ViewModels;

namespace UniversityClientApp.Controllers
{
    public class PaymentController : Controller
    {
        [HttpGet]
        public IActionResult Index(int educationId)
        {
            ViewBag.Education = APIClient.GetRequest<EducationViewModel>($"api/main/GetEducation?id={educationId}");
            ViewBag.Discipline = new MultiSelectList(APIClient.GetRequest<List<DisciplineViewModel>>
                ($"api/main/GetFilteredDisciplineList?id={educationId}"), "Id", "Name", "Price");
            return View();
        }

        [HttpPost]
        public IActionResult Index([Bind("DisciplineId", "Sum")] PaymentBindingModel model, decimal disciplineSum)
        {
            if (disciplineSum < model.Sum)
            {
                throw new Exception("Внесённая сумма не должна быть больше, чем сумма к оплате");
            }
            model.UserId = Program.User.Id;
            APIClient.PostRequest("api/payment/Pay", model);
            return Redirect("~/Home/Index");
        }

        public decimal CalcSum(int Id)
        {
            return APIClient.GetRequest<DisciplineViewModel>($"api/main/GetDiscipline?id={Id}").PriceToPay;
        }
    }
}
