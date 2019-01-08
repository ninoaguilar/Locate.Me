﻿using System;
using System.Collections.Generic;
using CompanyService.Abstractions.Repository;
using CompanyService.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyService.Test
{
    public class TestMemoryRepository
    {
        public readonly CompanyServiceContext _context;

        public TestMemoryRepository(string DbName)
        {
            var options = new DbContextOptionsBuilder<CompanyServiceContext>()
                .UseInMemoryDatabase(databaseName: DbName)
                .Options;

            _context = new CompanyServiceContext(options);
            AddTestData(_context);
        }

        internal static void AddTestData(CompanyServiceContext apiContext)
        {
            Guid testCompany1Id = Guid.Parse("64ab7258-2669-46fa-b408-10635b1e67ae");
            Guid testCompany2Id = Guid.Parse("1da8b16b-7173-4ced-94c4-5669584d8249");

            var testCompany1 = new Company
            {
                Id = testCompany1Id,
                Name = "Test Company One",
                Address = "123 Main St, Logan, UT",
                PhoneNumber = "5551234567"
            };

            var testCompany2 = new Company
            {
                Id = testCompany2Id,
                Name = "Test Company Two",
                Address = "543 Main St, Logan, UT",
                PhoneNumber = "5559876543"
            };

            var testEmployee1 = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = "Joeseph",
                PreferredName = "Joe",
                LastName = "Schmo",
                DayOfBirth = new DateTime(1995, 06, 16),
                Role = "Plummer",
                AboutSnippet = "Sample About Snippet. The quick brown fox jump over the lazy dog.",
                CompanyId = testCompany1Id,
                Company = testCompany1
            };

            var testEmployee2 = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = "Bober",
                PreferredName = "Bob",
                LastName = "Dinglberry",
                DayOfBirth = new DateTime(1992, 12, 01),
                Role = "Electrician",
                AboutSnippet = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet.",
                CompanyId = testCompany1Id,
                Company = testCompany1
            };

            var testEmployee3 = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = "Timothy",
                PreferredName = "Tim",
                LastName = "Doe",
                DayOfBirth = new DateTime(1990, 01, 12),
                Role = "Driver",
                AboutSnippet = "Morbi sit amet ante quis velit aliquam blandit. Proin a sem sed leo laoreet.",
                CompanyId = testCompany2Id,
                Company = testCompany2
            };

            apiContext.Companies.Add(testCompany1);
            apiContext.Companies.Add(testCompany2);
            apiContext.Employees.Add(testEmployee1);
            apiContext.Employees.Add(testEmployee2);
            apiContext.Employees.Add(testEmployee3);
            apiContext.SaveChanges();
        }
    }
}
