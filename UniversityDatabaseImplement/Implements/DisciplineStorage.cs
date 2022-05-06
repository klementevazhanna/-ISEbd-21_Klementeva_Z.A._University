using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UniversityContracts.BindingModels;
using UniversityContracts.StoragesContracts;
using UniversityContracts.ViewModels;
using UniversityDatabaseImplement.Models;

namespace UniversityDatabaseImplement.Implements
{
    public class DisciplineStorage : IDisciplineStorage
    {
        public List<DisciplineViewModel> GetFullList()
        {
            using var context = new UniversityDatabase();
            return context.Disciplines
                .Include(rec => rec.EducationsDisciplines)
                .ThenInclude(rec => rec.Education)
                .Include(rec => rec.Payments)
                .Select(CreateModel)
                .ToList();
        }

        public List<DisciplineViewModel> GetFilteredList(DisciplineBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new UniversityDatabase();
            return context.Disciplines
                .Include(rec => rec.EducationsDisciplines)
                .ThenInclude(rec => rec.Education)
                .Include(rec => rec.Payments)
                .Where(rec => model.EducationId.HasValue && rec.EducationsDisciplines.Any(rec => rec.EducationId == model.EducationId.Value))
                .Select(CreateModel)
                .ToList();
        }

        public DisciplineViewModel GetElement(DisciplineBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new UniversityDatabase();
            var discipline = context.Disciplines
                .Include(rec => rec.EducationsDisciplines)
                .ThenInclude(rec => rec.Education)
                .Include(rec => rec.Payments)
                .FirstOrDefault(rec => rec.Id == model.Id || rec.Name == model.Name);
            return discipline != null ? CreateModel(discipline) : null;
        }

        public void Insert(DisciplineBindingModel model)
        {
            using var context = new UniversityDatabase();
            context.Disciplines.Add(CreateModel(model, new Discipline()));
            context.SaveChanges();
        }

        public void Update(DisciplineBindingModel model)
        {
            using var context = new UniversityDatabase();
            var discipline = context.Disciplines.FirstOrDefault(rec => rec.Id == model.Id || rec.Name == model.Name);
            if (discipline == null)
            {
                throw new Exception("Дисциплина не найдена");
            }

            CreateModel(model, discipline);
            context.SaveChanges();
        }

        public void Delete(DisciplineBindingModel model)
        {
            using var context = new UniversityDatabase();
            var discipline = context.Disciplines.FirstOrDefault(rec => rec.Id == model.Id || rec.Name == model.Name);
            if (discipline == null)
            {
                throw new Exception("Дисциплина не найдена");
            }

            context.Disciplines.Remove(discipline);
            context.SaveChanges();
        }

        private static DisciplineViewModel CreateModel(Discipline discipline)
        {
            return new DisciplineViewModel
            {
                Id = discipline.Id,
                Name = discipline.Name,
                Price = discipline.Price,
                PriceToPay = discipline.Price - discipline.Payments.Sum(rec => rec.Sum)
            };
        }

        private static Discipline CreateModel(DisciplineBindingModel model, Discipline discipline)
        {
            discipline.Name = model.Name;
            discipline.Price = model.Price;
            return discipline;
        }
    }
}
