using System;
using System.Collections.Generic;
using CompanyService.Models;

namespace CompanyService.Persistence
{
    public class MemoryCompanyRepository : ICompanyRepository
    {
        #region Properties
        protected static ICollection<Company> _companies;
        #endregion

        #region Constructors
        public MemoryCompanyRepository()
        {
            if (_companies == null)
            {
                _companies = new List<Company>();
            }
        }

        public MemoryCompanyRepository(ICollection<Company> companies)
        {
            _companies = companies;
        }
        #endregion


        public void AddCompanies(Company company)
        {
            _companies.Add(company);
        }

        public IEnumerable<Company> GetCompanies()
        {
            return _companies;
        }
    }
}
