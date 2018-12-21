using System;
using System.Collections.Generic;
using Xunit;
using CompanyService.Models;
using CompanyService.Controllers;
using System.Linq;

namespace CompanyService.Test
{
    public class CompanyControllerTest
    {
        CompanyController controller = new CompanyController();

        [Fact]
        public void QueryCompanyListReturnsCorrectCompanies()
        {
            List<Company> companies = new List<Company>(
                    controller.GetAllCompanies());

            Assert.Equal(companies.Count, 2);
        }

        [Fact]
        public async void CreateCompanyAddsCompanyToList()
        {
            CompanyController controller = new CompanyController();

            var companies = (IEnumerable<Company>)
                (await controller.GetAllCompanies() as ObjectResult).Value;

            List<Company> originalCompanies = new List<Company>(companies);

            Company additionalCompany = new Company("sample");
            var result = await controller.CreateCompany(additionalCompany);

            var newCompanyRaw = (IEnumerable<Company>)
                (await controller.GetAllCompanies as ObjectResult).Value;

            var CompaniesWithAdditional = new List<Company>(newCompanyRaw);
            Assert.Equal(CompaniesWithAdditional.Count, originalCompanies + 1);

            var sampleCompany = CompaniesWithAdditional.FirstOrDefault(
                target => target.Name == "sample");
            Assert.NotNull(sampleCompany);
        }
    }
}
