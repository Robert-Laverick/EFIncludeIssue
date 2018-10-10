using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFIncludeIssue.Model
{
    public class TestDbContext: DbContext
    {
        public DbSet<Document> Documents { get; set; }
        public DbSet<MetaInfo> Meta { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Document>().HasMany(d => d.MetaInfo).WithRequired(m => m.Document);
        }
    }
}
