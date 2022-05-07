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
    public class PaymentStorage : IPaymentStorage
    {
        public List<PaymentViewModel> GetFullList()
        {
            using var context = new UniversityDatabase();
            return context.Payments
                .Include(rec => rec.User)
                .Include(rec => rec.Discipline)
                .Select(CreateModel)
                .ToList();
        }

        public List<PaymentViewModel> GetFilteredList(PaymentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new UniversityDatabase();
            return context.Payments
                .Include(rec => rec.User)
                .Include(rec => rec.Discipline)
                .Where(rec => rec.Sum == model.Sum)
                .Select(CreateModel)
                .ToList();
        }

        public PaymentViewModel GetElement(PaymentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new UniversityDatabase();
            var payment = context.Payments
                .Include(rec => rec.User)
                .Include(rec => rec.Discipline)
                .FirstOrDefault(rec => rec.Id == model.Id);
            return payment != null ? CreateModel(payment) : null;
        }

        public void Insert(PaymentBindingModel model)
        {
            using var context = new UniversityDatabase();
            context.Payments.Add(CreateModel(model, new Payment()));
            context.SaveChanges();
        }

        public void Update(PaymentBindingModel model)
        {
            using var context = new UniversityDatabase();
            var payment = context.Payments.FirstOrDefault(rec => rec.Id == model.Id);
            if (payment == null)
            {
                throw new Exception("Платёж не найден");
            }
            CreateModel(model, payment);
            context.SaveChanges();
        }

        public void Delete(PaymentBindingModel model)
        {
            using var context = new UniversityDatabase();
            var payment = context.Payments.FirstOrDefault(rec => rec.Id == model.Id);
            if (payment == null)
            {
                throw new Exception("Платёж не найден");
            }
            context.Payments.Remove(payment);
            context.SaveChanges();
        }

        private static PaymentViewModel CreateModel(Payment payment)
        {
            return new PaymentViewModel
            {
                Id = payment.Id,
                Sum = payment.Sum,
                DisciplineId = payment.DisciplineId,
                UserId = payment.UserId
            };
        }

        private static Payment CreateModel(PaymentBindingModel model, Payment payment)
        {
            payment.Sum = model.Sum.Value;
            payment.DisciplineId = model.DisciplineId;
            payment.UserId = model.UserId;
            return payment;
        }
    }
}
