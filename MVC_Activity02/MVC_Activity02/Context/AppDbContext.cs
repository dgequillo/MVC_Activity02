using MVC_Activity02.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MVC_Activity02.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("WinFormCustomerDb")
        {
        }

        public DbSet<Customer> Customers { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<Customer>()
                .ToTable("Customers");
            modelBuilder.Entity<Customer>().HasKey(c => c.Id);
            modelBuilder.Entity<Customer>().Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Customer>().Property(c => c.Firstname).HasMaxLength(64);
            modelBuilder.Entity<Customer>().Property(c => c.Lastname).HasMaxLength(64);
            modelBuilder.Entity<Customer>().Property(c => c.Address).HasMaxLength(100);
            modelBuilder.Entity<Customer>().Property(c => c.Status).HasMaxLength(10);
            modelBuilder.Entity<Customer>().Property(c => c.CreatedDate).HasColumnType("datetime2");
            modelBuilder.Entity<Customer>().Property(c => c.UpdatedDate).HasColumnType("datetime2");
        }
    }
}
