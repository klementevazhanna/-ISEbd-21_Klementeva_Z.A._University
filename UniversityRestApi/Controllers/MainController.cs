using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using UniversityContracts.BindingModels;
using UniversityContracts.BusinessLogicContracts;
using UniversityContracts.ViewModels;

namespace UniversityRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : Controller
    {
        private readonly IEducationLogic _educationLogic;

        private readonly IDisciplineLogic _disciplineLogic;

        public MainController(IEducationLogic educationLogic, IDisciplineLogic disciplineLogic)
        {
            _educationLogic = educationLogic;
            _disciplineLogic = disciplineLogic;
        }

        [HttpGet]
        public List<EducationViewModel> GetEducations(int userId) => _educationLogic.Read(new EducationBindingModel { UserId = userId });

        [HttpGet]
        public List<DisciplineViewModel> GetDisciplineList() => _disciplineLogic.Read(null)?.ToList();

        [HttpGet]
        public List<DisciplineViewModel> GetFilteredDisciplineList(int id) =>
            _disciplineLogic.Read(new DisciplineBindingModel { EducationId = id })?.ToList();

        [HttpGet]
        public DisciplineViewModel GetDiscipline(int id) => _disciplineLogic.Read(new DisciplineBindingModel { Id = id })?[0];

        [HttpGet]
        public EducationViewModel GetEducation(int id) => _educationLogic.Read(new EducationBindingModel { Id = id })?[0];

        [HttpGet]
        public EducationViewModel GetEducationByName(string name) => _educationLogic.Read(new EducationBindingModel { Name = name })?[0];

        [HttpPost]
        public void CreateEducation(EducationBindingModel model) => _educationLogic.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteEducation(EducationBindingModel model) => _educationLogic.Delete(model);
    }
}
