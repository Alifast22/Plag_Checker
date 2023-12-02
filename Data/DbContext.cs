using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PlagChecker.Models;


namespace PlagChecker.Data
{
    public class YourDbContext : DbContext
    {
        public YourDbContext() : base("name=DefaultConnection")
        {
        }

        public DbSet<Student> CodeSubmissions { get; set; }
     //   public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Custom model configuration (if needed)
        }

        public System.Data.Entity.DbSet<PlagChecker.Models.Result> Results { get; set; }
    }
}