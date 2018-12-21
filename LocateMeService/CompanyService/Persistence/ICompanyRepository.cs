using System;
using System.Collections.Generic;
using CompanyService.Models;

namespace CompanyService.Persistence
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetCompanies();
        void AddCompanies(Company company);
    }
}
