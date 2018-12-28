using System;
using System.Collections.Generic;
using CompanyService.Models;
using CompanyService.Persistence;

namespace CompanyService.Test
{
    public class TestMemoryRepository : MemoryCompanyRepository
    {
        public TestMemoryRepository() : base(CreateInitialFake())
        {
        }

        private static ICollection<Company> CreateInitialFake()
        {
            var companies = new List<Company>
            {
                new Company("Test Company One"),
                new Company("Test Company Two")
            };

            return companies;
        }
    }
}
