using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDatabaseImplement.Models
{
    public class CostItem
    {
        public int Id { get; set; }

        public decimal Sum { get; set; }

        public string Name { get; set; }

        [ForeignKey("CostItemId")]
        public virtual List<CostItemEducation> CostItemsEducations { get; set; }
    }
}
