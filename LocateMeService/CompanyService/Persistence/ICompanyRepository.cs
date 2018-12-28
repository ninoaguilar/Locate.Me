using System;
using System.Collections.Generic;
using CompanyService.Models;

namespace CompanyService.Persistence
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> ListAll();
        Company Get(Guid id);
        Company Add(Company company);
        Company Update(Company company);
        Company Delete(Guid id);
    }
}
