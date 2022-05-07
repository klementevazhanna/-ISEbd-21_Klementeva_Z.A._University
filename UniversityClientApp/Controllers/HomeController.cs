using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UniversityClientApp.Models;
using UniversityContracts.BindingModels;
using UniversityContracts.ViewModels;

namespace UniversityClientApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (Program.User == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<List<EducationViewModel>>($"api/main/GetEducations?userId={Program.User.Id}"));
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            if (Program.User == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(Program.User);
        }

        [HttpPost]
        public void Privacy(string email, string password, string FIO)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(FIO))
            {
                Program.User.FIO = FIO;
                Program.User.Email = email;
                Program.User.Password = password;
                APIClient.PostRequest("api/User/updatedata", new UserBindingModel
                {
                    Id = Program.User.Id,
                    FIO = FIO,
                    Email = email,
                    Password = password
                });
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите логин, пароль и ФИО");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }

        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }

        [HttpPost]
        public void Enter(string email, string password)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                Program.User = APIClient.GetRequest<UserViewModel>($"api/User/login?login={email}&password={password}");

                if (Program.User == null)
                {
                    throw new Exception("Неверный логин/пароль");
                }
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите логин, пароль");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public void Register(string FIO, string password, string email)
        {
            if (!string.IsNullOrEmpty(FIO) && !string.IsNullOrEmpty(password)
            && !string.IsNullOrEmpty(email))
            {
                APIClient.PostRequest("api/user/register", new UserBindingModel
                {
                    FIO = FIO,
                    Email = email,
                    Password = password
                });
                Response.Redirect("Enter");
                return;
            }
            throw new Exception("Введите логин, пароль и электронную почту");
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Disciplines = new MultiSelectList(APIClient.GetRequest<List<DisciplineViewModel>>($"api/main/GetDisciplineList"),
                "Id", "Name", "Price");
            return View(new EducationViewModel());
        }

        [HttpPost]
        public void Create(DateTime datepicker, [Bind("DisciplineIds", "Name")] EducationViewModel model)
        {
            List<DisciplineViewModel> disciplines = model.DisciplineIds.
                Select(rec => APIClient.GetRequest<DisciplineViewModel>($"api/main/GetDiscipline?id={rec}")).ToList();
            if (string.IsNullOrEmpty(model.Name) || model.DisciplineIds.Count == 0)
            {
                return;
            }
            APIClient.PostRequest("api/main/CreateEducation", new EducationBindingModel
            {
                UserId = Program.User.Id,
                Cost = disciplines.Sum(rec => rec.Price),
                Count = disciplines.Count,
                Name = model.Name,
                EducationDate = datepicker,
                EducationDisciplines = disciplines.ToDictionary(rec => rec.Id, rec => rec.Name)
            });
            Response.Redirect("Index");
        }

        [HttpGet]
        public void Delete(int Id)
        {
            APIClient.PostRequest($"api/main/deleteeducation", new EducationBindingModel
            {
                Id = Id
            });
            Response.Redirect("../Index");
        }

        [HttpGet]
        public IActionResult Update(int Id)
        {
            var disciplines = new MultiSelectList(APIClient.GetRequest<List<DisciplineViewModel>>($"api/main/GetDisciplineList"),
                "Id", "Name", "Price");
            var education = APIClient.GetRequest<EducationViewModel>($"api/main/GetEducation?id={Id}");
            foreach (var elem in disciplines)
            {
                if (education.DisciplineIds.Contains(Convert.ToInt32(elem.Value)))
                {
                    elem.Selected = true;
                }
            }
            ViewBag.Disciplines = disciplines;
            return View(education);
        }

        [HttpPost]
        public void Update(DateTime datepicker, [Bind("DisciplineIds", "Name", "Id")] EducationViewModel model)
        {
            if (string.IsNullOrEmpty(model.Name) || model.DisciplineIds == null || model.DisciplineIds.Count == 0)
            {
                return;
            }
            List<DisciplineViewModel> disciplines = model.DisciplineIds.
                Select(rec => APIClient.GetRequest<DisciplineViewModel>($"api/main/GetDiscipline?id={rec}")).ToList();

            APIClient.PostRequest("api/main/CreateEducation", new EducationBindingModel
            {
                Id = model.Id,
                UserId = Program.User.Id,
                Cost = disciplines.Sum(rec => rec.Price),
                Count = disciplines.Count,
                Name = model.Name,
                EducationDate = datepicker,
                EducationDisciplines = disciplines.ToDictionary(rec => rec.Id, rec => rec.Name)
            });
            Response.Redirect("../Index");
        }
    }
}
