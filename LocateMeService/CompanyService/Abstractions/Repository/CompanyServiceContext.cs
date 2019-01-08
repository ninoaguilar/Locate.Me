using System;
using CompanyService.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyService.Abstractions.Repository
{
    public class CompanyServiceContext : DbContext
    {
        public CompanyServiceContext(DbContextOptions<CompanyServiceContext> options) 
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        #region Entities
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        #endregion

    }
}
