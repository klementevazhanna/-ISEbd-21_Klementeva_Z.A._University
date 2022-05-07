using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using UniversityContracts.BindingModels;
using UniversityContracts.ViewModels;

namespace UniversityClientApp.Controllers
{
    public class ReportController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public ReportController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult Index()
        {
            if (Program.User == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.EducationIds = new MultiSelectList(APIClient.GetRequest<List<EducationViewModel>>
                ($"api/main/geteducations?UserId={Program.User.Id}"), "Id", "Name");
            return View();
        }

        public IActionResult Email()
        {
            if (Program.User == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View();
        }

        [HttpPost]
        public IActionResult ReportDoc([Bind("EducationIds")] ReportBindingModel model)
        {
            model.FileName = @"..\UniversityClientApp\wwwroot\report\Report.doc";
            APIClient.PostRequest("api/report/MakeDoc", model);

            var fileName = "Report.doc";
            var filePath = _environment.WebRootPath + @"\report\" + fileName;
            return PhysicalFile(filePath, "application/doc", fileName);
        }

        [HttpPost]
        public IActionResult ReportXls([Bind("EducationIds")] ReportBindingModel model)
        {
            model.FileName = @"..\UniversityClientApp\wwwroot\report\Report.xls";
            APIClient.PostRequest("api/report/MakeExcel", model);

            var fileName = "Report.xls";
            var filePath = _environment.WebRootPath + @"\report\" + fileName;
            return PhysicalFile(filePath, "application/xls", fileName);
        }

        [HttpPost]
        public IActionResult ReportOnView([Bind("DateTo,DateFrom")] ReportBindingModel model)
        {
            model.UserId = Program.User.Id;
            model.FileName = @"..\UniversityClientApp\wwwroot\report\Report.pdf";
            APIClient.PostRequest("api/report/MakePdf", model);
            ViewBag.Report = model.FileName;
            return View("Email");
        }

        [HttpPost]
        public IActionResult SendMail([Bind("DateTo,DateFrom")] ReportBindingModel model)
        {
            model.UserId = Program.User.Id;
            model.UserEmail = Program.User.Email;
            model.FileName = @"..\UniversityClientApp\wwwroot\report\Report.pdf";
            APIClient.PostRequest("api/report/SendMail", model);
            return Redirect("~/Home/Index");
        }
    }
}
