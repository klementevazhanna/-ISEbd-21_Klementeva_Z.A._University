using Microsoft.AspNetCore.Mvc;
using System.Linq;
using UniversityBusinessLogic.BusinessLogic;
using UniversityContracts.BindingModels;
using UniversityContracts.BusinessLogicContracts;

namespace UniversityRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IReportLogic _reportLogic;

        private readonly IEducationLogic _educationLogic;

        public ReportController(IReportLogic reportLogic, IEducationLogic educationLogic)
        {
            _reportLogic = reportLogic;
            _educationLogic = educationLogic;
        }

        [HttpPost]
        public void MakeDoc(ReportBindingModel model) => _reportLogic.SaveEducationsToWordFile(model);

        [HttpPost]
        public void MakeExcel(ReportBindingModel model) => _reportLogic.SaveEducationsToExcelFile(model);

        [HttpPost]
        public void MakePdf(ReportBindingModel model) => _reportLogic.SaveEducationsToPdfFile(model);

        [HttpPost]
        public void SendMail(ReportBindingModel model)
        {
            _reportLogic.SaveEducationsToPdfFile(model);
            MailLogic.MailSendAsync(new MailSendInfoBindingModel
            {
                MailAddress = model.UserEmail,
                Subject = "Отчет",
                Text = "Отчет по обучениям",
                ReportFile = model.FileName
            });
        }

        [HttpGet]
        public ReportBindingModel GetEducations(int UserId)
        {
            return new ReportBindingModel
            {
                UserId = UserId,
                EducationIds = _educationLogic.Read(new EducationBindingModel { UserId = UserId }).Select(rec => rec.Id).ToList()
            };
        }
    }
}
