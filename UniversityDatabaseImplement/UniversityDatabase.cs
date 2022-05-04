using Microsoft.EntityFrameworkCore;
using UniversityDatabaseImplement.Models;

namespace UniversityDatabaseImplement
{
    public class UniversityDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Server=localhost;Database=universitydb;Trusted_Connection=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<User> Users { set; get; }

        public virtual DbSet<Education> Educations { set; get; }
    }
}
