using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.RegularExpressions;
using UniversityContracts.BindingModels;
using UniversityContracts.BusinessLogicContracts;
using UniversityContracts.ViewModels;

namespace UniversityRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserLogic _logic;

        private readonly int _passwordMaxLength = 20;

        private readonly int _passwordMinLength = 5;

        public UserController(IUserLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        public UserViewModel Login(string login, string password) => _logic.Read(new UserBindingModel
        {
            Email = login,
            Password = password 
        })?[0];

        [HttpPost]
        public void Register(UserBindingModel model)
        {
            CheckData(model);
            _logic.CreateOrUpdate(model);
        }

        [HttpPost]
        public void UpdateData(UserBindingModel model)
        {
            CheckData(model);
            _logic.CreateOrUpdate(model);
        }

        private void CheckData(UserBindingModel model)
        {
            if (!Regex.IsMatch(model.Email, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$"))
            {
                throw new Exception("В качестве логина должна быть указана почта");
            }
            if (model.Password.Length > _passwordMaxLength || model.Password.Length <
            _passwordMinLength || !Regex.IsMatch(model.Password,
            @"^((\w+\d+\W+)|(\w+\W+\d+)|(\d+\w+\W+)|(\d+\W+\w+)|(\W+\w+\d+)|(\W+\d+\w+))[\w\d\W]*$"))
            {
                throw new Exception($"Пароль длиной от {_passwordMinLength} до {_passwordMaxLength }" +
                    $" должен состоять и из цифр, букв и небуквенных символов");
            }
        }
    }
}
