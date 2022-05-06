using System;
using System.Collections.Generic;
using System.Linq;
using UniversityContracts.BindingModels;
using UniversityContracts.StoragesContracts;
using UniversityContracts.ViewModels;
using UniversityDatabaseImplement.Models;

namespace UniversityDatabaseImplement.Implements
{
    public class UserStorage : IUserStorage
    {
        public List<UserViewModel> GetFullList()
        {
            using var context = new UniversityDatabase();
            return context.Users
                .Select(CreateModel)
                .ToList();
        }

        public List<UserViewModel> GetFilteredList(UserBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new UniversityDatabase();
            return context.Users
                .Where(rec => rec.Email == model.Email & rec.Password == model.Password)
                .Select(CreateModel)
                .ToList();
        }

        public UserViewModel GetElement(UserBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new UniversityDatabase();
            var user = context.Users.FirstOrDefault(rec => rec.Id == model.Id || rec.Email == model.Email);
            return user != null ? CreateModel(user) : null;
        }

        public void Insert(UserBindingModel model)
        {
            using var context = new UniversityDatabase();
            context.Users.Add(CreateModel(model, new User()));
            context.SaveChanges();
        }

        public void Update(UserBindingModel model)
        {
            using var context = new UniversityDatabase();
            var user = context.Users.FirstOrDefault(rec => rec.Id == model.Id || rec.Email == model.Email);
            if (user == null)
            {
                throw new Exception("Пользователь не найден");
            }

            CreateModel(model, user);
            context.SaveChanges();
        }

        public void Delete(UserBindingModel model)
        {
            using var context = new UniversityDatabase();
            var user = context.Users.FirstOrDefault(rec => rec.Id == model.Id || rec.Email == model.Email);
            if (user == null)
            {
                throw new Exception("Пользователь не найден");
            }

            context.Users.Remove(user);
            context.SaveChanges();
        }

        private static UserViewModel CreateModel(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                FIO = user.FIO,
                Email = user.Email,
                Password = user.Password
            };
        }

        private static User CreateModel(UserBindingModel model, User user)
        {
            user.FIO = model.FIO;
            user.Email = model.Email;
            user.Password = model.Password;
            return user;
        }
    }
}
