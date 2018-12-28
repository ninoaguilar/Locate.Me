using System;
using System.Collections.Generic;
using System.Linq;
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


        #region Methods
        public IEnumerable<Company> ListAll()
        {
            return _companies;
        }

        public Company Add(Company company)
        {
            _companies.Add(company);
            return company;
        }

        public Company Get(Guid id)
        {
            return _companies.FirstOrDefault(t => t.Id == id);
        }

        public Company Delete(Guid id)
        {
            var q = _companies.Where(t => t.Id == id);
            Company company = null;

            if (q.Any())
            {
                company = q.First();
                _companies.Remove(company);
            }

            return company;
        }

        public Company Update(Company company)
        {
            Company updatedCompany = this.Delete(company.Id);

            if(updatedCompany != null)
            {
                updatedCompany = this.Add(company);
            }

            return updatedCompany;
        }
        #endregion

    }
}
