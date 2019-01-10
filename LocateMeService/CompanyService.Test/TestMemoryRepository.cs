using System;
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
            _context.Database.EnsureCreated();
        }
    }
}
