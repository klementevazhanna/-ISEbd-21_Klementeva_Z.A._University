using System.Collections.Generic;

namespace UniversityContracts.ViewModels
{
    public class CostItemViewModel
    {
        public int Id { get; set; }

        public decimal Sum { get; set; }

        public string Name { get; set; }

        public Dictionary<int, string> CostItemEducations { get; set; }
    }
}
