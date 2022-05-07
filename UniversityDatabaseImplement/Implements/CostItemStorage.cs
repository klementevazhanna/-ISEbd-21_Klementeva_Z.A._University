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
    public class CostItemStorage : ICostItemStorage
    {
        public CostItemViewModel GetElement(CostItemBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new UniversityDatabase();
            var costItem = context.CostItems
                .Include(x => x.CostItemsEducations)
                .ThenInclude(x => x.Education)
                .FirstOrDefault(rec => rec.Id == model.Id || rec.Name == model.Name);
            return costItem != null ? CreateModel(costItem) : null;
        }

        public void Insert(CostItemBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                CreateModel(model, new CostItem(), context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(CostItemBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var costItem = context.CostItems.FirstOrDefault(rec => rec.Id == model.Id || rec.Name == model.Name);
                if (costItem == null)
                {
                    throw new Exception("Не найдено");
                }
                CreateModel(model, costItem, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Delete(CostItemBindingModel model)
        {
            using var context = new UniversityDatabase();
            var costItem = context.CostItems.FirstOrDefault(rec => rec.Id == model.Id || rec.Name == model.Name);
            if (costItem == null)
            {
                throw new Exception("Не найдено");
            }
            context.CostItems.Remove(costItem);
            context.SaveChanges();
        }

        public int Count()
        {
            using var context = new UniversityDatabase();
            return context.CostItems.Count();
        }

        private static CostItemViewModel CreateModel(CostItem costItem)
        {
            return new CostItemViewModel
            {
                Id = costItem.Id,
                Name = costItem.Name,
                Sum = costItem.Sum,
                CostItemEducations = costItem.CostItemsEducations.ToDictionary(x => x.EducationId, x => x.Education.Name)
            };
        }

        private static CostItem CreateModel(CostItemBindingModel model, CostItem costItem, UniversityDatabase context)
        {
            costItem.Sum = model.Sum;
            costItem.Name = model.Name;

            if (costItem.Id == 0)
            {
                context.CostItems.Add(costItem);
                context.SaveChanges();
            }

            if (model.Id.HasValue)
            {
                List<CostItemEducation> costItemEducations = context.CostItemsEducations
                    .Where(rec => rec.CostItemId == model.Id.Value)
                    .ToList();

                // удаляем те, которых нет в модели
                context.CostItemsEducations
                    .RemoveRange(costItemEducations
                    .Where(rec => !model.CostItemEducations.ContainsKey(rec.EducationId)).ToList());
                context.SaveChanges();

                foreach (var education in costItemEducations)
                {
                    model.CostItemEducations.Remove(education.EducationId);
                }
                context.SaveChanges();
            }

            foreach (var education in model.CostItemEducations)
            {
                context.CostItemsEducations.Add(new CostItemEducation
                {
                    CostItemId = costItem.Id,
                    EducationId = education.Key
                });
                context.SaveChanges();
            }

            return costItem;
        }
    }
}
