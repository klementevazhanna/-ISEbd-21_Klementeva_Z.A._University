using System.Collections.Generic;

namespace UniversityContracts.BindingModels
{
    public class CostItemBindingModel
    {
        public int? Id { get; set; }

        public decimal Sum { get; set; }

        public string Name { get; set; }

        public Dictionary<int, string> CostItemEducations { get; set; }
    }
}
