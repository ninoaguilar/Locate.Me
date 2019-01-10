using System;
using CompanyService.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyService.Abstractions.Repository
{
    public static class ModelBuilderExtensions
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            var testCompany1Id = Guid.Parse("86c06754-d473-4e77-9350-d61ba8cf190b");
            var testCompany2Id = Guid.Parse("e19aa749-06e7-4f07-8ef0-7d08c8e0150b");

            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = testCompany1Id,
                    Name = "Test Company One",
                    Address = "123 Main St, Logan, UT",
                    PhoneNumber = "5551234567"
                },
                new Company
                {
                    Id = testCompany2Id,
                    Name = "Test Company Two",
                    Address = "543 Main St, Logan, UT",
                    PhoneNumber = "5559876543"
                }
            );
        }
    }
}
