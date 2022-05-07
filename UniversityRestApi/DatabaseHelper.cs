using System;
using System.Collections.Generic;
using System.Linq;
using UniversityContracts.BindingModels;
using UniversityContracts.BusinessLogicContracts;
using UniversityContracts.ViewModels;

namespace UniversityRestApi
{
    public class DatabaseHelper
    {
        private readonly IDisciplineLogic _disciplineLogic;

        private readonly IEducationLogic _educationLogic;

        private readonly ICostItemLogic _costItemLogic;

        public DatabaseHelper(IDisciplineLogic disciplineLogic, IEducationLogic educationLogic, ICostItemLogic costItemLogic)
        {
            _disciplineLogic = disciplineLogic;
            _educationLogic = educationLogic;
            _costItemLogic = costItemLogic;
        }

        public void Load()
        {
            DisciplinesLoad();
            CostUtemLoad();
        }

        public void DisciplinesLoad()
        {
            var list = _disciplineLogic.Read(null);
            List<DisciplineBindingModel> disciplines = new()
            {
                new DisciplineBindingModel { Name = "Java для начинающих", Price = 2000 },
                new DisciplineBindingModel { Name = "Python-разработка", Price = 1599 },
                new DisciplineBindingModel { Name = "Тестирование ПО для начинающих", Price = 1090 },
                new DisciplineBindingModel { Name = "SQL для начинающих", Price = 1090 },
                new DisciplineBindingModel { Name = "Разработка Telegram-ботов", Price = 4690 },
                new DisciplineBindingModel { Name = "Сети с нуля", Price = 999 },
                new DisciplineBindingModel { Name = "Изучение С++ с нуля", Price = 999 },
                new DisciplineBindingModel { Name = "Проектный менеджмент", Price = 2060 },
                new DisciplineBindingModel { Name = "Введение в коучинг", Price = 999 },
                new DisciplineBindingModel { Name = "Управление конфликтами", Price = 1290 },
                new DisciplineBindingModel { Name = "Искусство тайм-менеджмента", Price = 1060 }
            };
            if (list.Count < disciplines.Count)
            {
                try
                {
                    foreach (var res in disciplines)
                    {
                        var listName = list.Select(x => x.Name).ToList();
                        if (!listName.Contains(res.Name))
                        {
                            _disciplineLogic.CreateOrUpdate(res);
                        }
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        public void CostUtemLoad()
        {
            List<CostItemBindingModel> list = new()
            {
                new CostItemBindingModel { Name = "Книга «И. Кант»", Sum = 499 },
                new CostItemBindingModel { Name = "Книга исторических подходов", Sum = 399 },
                new CostItemBindingModel { Name = "Книга «Эффективный Python»", Sum = 800 },
                new CostItemBindingModel { Name = "Книга «Быстрый Java»", Sum = 755 },
                new CostItemBindingModel { Name = "Книга «Чистый код»", Sum = 1010 },
                new CostItemBindingModel { Name = "Книга «Искусство программирования»", Sum = 6666 },
                new CostItemBindingModel { Name = "Ручка", Sum = 20 },
                new CostItemBindingModel { Name = "Карандаш", Sum = 10 },
                new CostItemBindingModel { Name = "Тетрадь", Sum = 21 },
                new CostItemBindingModel { Name = "Стёрка", Sum = 50 },
                new CostItemBindingModel { Name = "Книга «System Design»", Sum = 1999 }
            };
            var educations = _educationLogic.Read(new EducationBindingModel { NoCost = true });
            if ((educations == null || educations.Count == 0) && _costItemLogic.Count() >= list.Count)
                return;
            try
            {
                Random rand = new();
                foreach (EducationViewModel education in educations)
                {
                    int costCount = rand.Next(1, list.Count - 1);
                    int start = rand.Next(0, list.Count - 2);
                    if (start > costCount)
                    {
                        int temp = start;
                        start = costCount;
                        costCount = temp;
                    }
                    for (int i = start; i <= costCount; ++i)
                    {
                        CostItemBindingModel cost = list[i];
                        if (cost.CostItemEducations == null)
                        {
                            var elem = _costItemLogic.GetElement(cost);
                            if (elem != null)
                            {
                                cost.CostItemEducations = elem.CostItemEducations;
                                cost.Id = elem.Id;
                            }
                            else
                            {
                                cost.CostItemEducations = new Dictionary<int, string>();
                            }
                        }
                        if (!cost.CostItemEducations.ContainsKey(education.Id))
                        {
                            cost.CostItemEducations.Add(education.Id, education.Name);
                        }
                    }
                }
                foreach (var cost in list)
                {
                    if (cost.CostItemEducations != null && cost.CostItemEducations.Count > 0)
                    {
                        _costItemLogic.CreateOrUpdate(cost);
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
