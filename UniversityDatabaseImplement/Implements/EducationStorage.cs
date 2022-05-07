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
    public class EducationStorage : IEducationStorage
    {
        public List<EducationViewModel> GetFullList()
        {
            using var context = new UniversityDatabase();
            return context.Educations
                .Include(rec => rec.User)
                .Include(rec => rec.EducationsDisciplines)
                .ThenInclude(rec => rec.Discipline)
                .ThenInclude(rec => rec.Payments)
                .Include(rec => rec.CostItemsEducations)
                .ThenInclude(rec => rec.CostItem)
                .Select(CreateModel)
                .ToList();
        }

        public List<EducationViewModel> GetFilteredList(EducationBindingModel model)
        {
            using var context = new UniversityDatabase();
            return context.Educations
                .Include(rec => rec.User)
                .Include(rec => rec.EducationsDisciplines)
                .ThenInclude(rec => rec.Discipline)
                .ThenInclude(rec => rec.Payments)
                .Include(rec => rec.CostItemsEducations)
                .ThenInclude(rec => rec.CostItem)
                .Where(rec =>
                 // сортируем по пользователю
                 (model.UserId.HasValue && !model.DateFrom.HasValue && model.PickedEducations == null && model.UserId == rec.UserId) ||
                // обучения без статей затрат для заполнения бд
                (!model.DateFrom.HasValue && model.PickedEducations == null && !model.UserId.HasValue && model.NoCost.HasValue && model.NoCost.Value && rec.CostItemsEducations.Count == 0))
                .Select(CreateModel)
                .ToList();
        }

        public EducationViewModel GetElement(EducationBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new UniversityDatabase();
            var education = context.Educations
                .Include(rec => rec.User)
                .Include(rec => rec.EducationsDisciplines)
                .ThenInclude(rec => rec.Discipline)
                .ThenInclude(rec => rec.Payments)
                .Include(rec => rec.CostItemsEducations)
                .ThenInclude(rec => rec.CostItem)
                .FirstOrDefault(rec => rec.Id == model.Id || rec.Name == model.Name);
            return education != null ? CreateModel(education) : null;
        }

        public List<EducationViewModel> GetFilteredByPickList(EducationBindingModel model)
        {
            using var context = new UniversityDatabase();
            return context.Educations
                .Include(rec => rec.User)
                .Include(rec => rec.EducationsDisciplines)
                .ThenInclude(rec => rec.Discipline)
                .ThenInclude(rec => rec.Payments)
                .Include(rec => rec.CostItemsEducations)
                .ThenInclude(rec => rec.CostItem)
                .Where(rec => model.PickedEducations.Contains(rec.Id)).Select(rec => new EducationViewModel
                {
                    Disciplines = rec.EducationsDisciplines
                    .Select(rec => new DisciplineViewModel
                    {
                        Name = rec.Discipline.Name,
                        Price = rec.Discipline.Price
                    })
                    .ToList(),
                    Name = rec.Name,
                    Cost = rec.Cost
                })
                .ToList();
        }

        public List<EducationViewModel> GetFilteredByDateList(EducationBindingModel model)
        {
            using var context = new UniversityDatabase();
            return context.Educations
                .Include(rec => rec.User)
                .Include(rec => rec.CostItemsEducations)
                .ThenInclude(rec => rec.CostItem)
                .Where(rec => model.DateFrom.HasValue && model.DateFrom.HasValue && model.DateFrom.Value.Date <= rec.EducationDate.Date && rec.EducationDate.Date <= model.DateTo.Value.Date && model.UserId == rec.UserId)
                .Select(rec => new EducationViewModel
                {
                    CostItems = rec.CostItemsEducations
                    .Select(rec => new CostItemViewModel
                    {
                        Name = rec.CostItem.Name,
                        Sum = rec.CostItem.Sum
                    })
                    .ToList(),
                    Name = rec.Name,
                    Cost = rec.Cost,
                    EducationDate = rec.EducationDate,
                    Count = rec.Count
                })
                .ToList();
        }

        public void Insert(EducationBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                CreateModel(model, new Education(), context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(EducationBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var education = context.Educations.FirstOrDefault(rec => rec.Id == model.Id || rec.Name == model.Name);
                if (education == null)
                {
                    throw new Exception("Обучение не найдено");
                }
                CreateModel(model, education, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Delete(EducationBindingModel model)
        {
            using var context = new UniversityDatabase();
            var education = context.Educations.FirstOrDefault(rec => rec.Id == model.Id || rec.Name == model.Name);
            if (education == null)
            {
                throw new Exception("Обучение не найдено");
            }
            context.Educations.Remove(education);
            context.SaveChanges();
        }

        private static EducationViewModel CreateModel(Education education)
        {
            return new EducationViewModel
            {
                Id = education.Id,
                Cost = education.Cost,
                CostToPay = education.Cost - education.EducationsDisciplines.Sum(rec => rec.Discipline.Payments.Sum(y => y.Sum)),
                Count = education.Count,
                Name = education.Name,
                EducationDate = education.EducationDate,
                EducationDisciplines = education.EducationsDisciplines.ToDictionary(rec => rec.DisciplineId, rec => rec.Discipline.Name),
                CostItemEducation = education.CostItemsEducations.ToDictionary(rec => rec.CostItemId, rec => rec.CostItem.Sum),
                CostItems = education.CostItemsEducations.Select(rec => new CostItemViewModel
                {
                    Id = rec.CostItemId,
                    Name = rec.CostItem.Name,
                    Sum = rec.CostItem.Sum
                }).ToList(),
                UserId = education.UserId,
                Disciplines = education.EducationsDisciplines.Select(rec => new DisciplineViewModel
                {
                    Id = rec.DisciplineId,
                    Name = rec.Discipline.Name,
                    Price = rec.Discipline.Price
                }).ToList(),
                DisciplineIds = education.EducationsDisciplines.Select(rec => rec.DisciplineId).ToList()
            };
        }

        private static Education CreateModel(EducationBindingModel model, Education education, UniversityDatabase context)
        {
            education.Cost = model.Cost;
            education.Count = model.Count;
            education.Name = model.Name;
            education.EducationDate = model.EducationDate;
            education.UserId = model.UserId.Value;

            if (education.Id == 0)
            {
                context.Educations.Add(education);
                context.SaveChanges();
            }

            if (model.Id.HasValue)
            {
                var educationDiscipline = context.EducationsDisciplines
                    .Where(rec => rec.EducationId == model.Id.Value)
                    .ToList();

                // удаляем те, которых нет в модели
                context.EducationsDisciplines.RemoveRange(educationDiscipline.Where(rec =>
                !model.EducationDisciplines.ContainsKey(rec.DisciplineId)).ToList());
                context.SaveChanges();

                foreach (var discipline in educationDiscipline)
                {
                    model.EducationDisciplines.Remove(discipline.DisciplineId);
                }
                context.SaveChanges();
            }

            foreach (var discipline in model.EducationDisciplines)
            {
                context.EducationsDisciplines.Add(new EducationDiscipline
                {
                    DisciplineId = discipline.Key,
                    EducationId = education.Id
                });
                context.SaveChanges();
            }

            return education;
        }
    }
}
