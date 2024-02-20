using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using HR.Entity;

namespace HR.Application.Infrastructure
{
    public class HrDbContext : DbContext
    {

        public HrDbContext(DbContextOptions<HrDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                   .HasOne(X => X.Gender)
                   .WithMany(x => x.Customers)
                   .HasForeignKey(x => x.GenderId).IsRequired();

            base.OnModelCreating(modelBuilder);
            new CustomerMap(modelBuilder.Entity<Customer>());
        }
    }
}
