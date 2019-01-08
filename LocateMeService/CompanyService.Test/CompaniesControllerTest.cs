using System;
using System.Collections.Generic;
using Xunit;
using CompanyService.Models;
using CompanyService.Controllers;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyService.Abstractions.Repository;

namespace CompanyService.Test
{
    public class CompanyControllerTest
    {
        [Fact]
        public async void Query_company_list_returns_correct_companies_async()
        {
            var testRepository = new TestMemoryRepository("Query_company_list_returns_correct_companies_async");

            CompaniesController controller = new CompaniesController(testRepository._context);

            var result = (IEnumerable<Company>)(await controller.GetAllCompaniesAsync() as ObjectResult).Value;
            var companies = new List<Company>(result);
            Assert.Equal(2, companies.Count);
            Assert.Equal("Test Company One", companies[0].Name);
            Assert.Equal("Test Company Two", companies[1].Name);
        }

        [Fact]
        public async void Get_company_retrieves_company_async()
        {
            var options = new DbContextOptionsBuilder<CompanyServiceContext>()
                .UseInMemoryDatabase(databaseName: "Get_company_retrieves_company_async")
                .Options;

            CompaniesController controller = new CompaniesController(new CompanyServiceContext(options));

            string sampleName = "Sample Company";
            Guid id = Guid.NewGuid();
            Company sampleCompany = new Company(sampleName, id);

            await controller.CreateCompanyAsync(sampleCompany);

            var resultCompany = (Company)(await controller.GetCompanyAsync(sampleCompany.Id) as ObjectResult).Value;

            Assert.Equal(resultCompany, sampleCompany);
        }

        [Fact]
        public async void Get_non_existent_company_returns_not_found_async()
        {
            var options = new DbContextOptionsBuilder<CompanyServiceContext>()
                .UseInMemoryDatabase(databaseName: "Get_non_existent_company_returns_not_found_async")
                .Options;

            CompaniesController controller = new CompaniesController(new CompanyServiceContext(options));

            Guid id = Guid.NewGuid();

            var resultCompany = await controller.GetCompanyAsync(id);
            var test = resultCompany;
            Assert.True(resultCompany is NotFoundResult);
        }

        [Fact]
        public async void Create_company_adds_company_to_list_async()
        {
            var options = new DbContextOptionsBuilder<CompanyServiceContext>()
                .UseInMemoryDatabase(databaseName: "Create_company_adds_company_to_list_async")
                .Options;

            CompaniesController controller = new CompaniesController(new CompanyServiceContext(options));

            var resultCompanies = (IEnumerable<Company>)(await controller.GetAllCompaniesAsync() as ObjectResult).Value;
            List<Company> originalCompanies = new List<Company>(resultCompanies);

            Company additionalCompany = new Company("sample");
            var result = controller.CreateCompanyAsync(additionalCompany).Result;
            Assert.Equal(201, (result as ObjectResult).StatusCode);

            var updatedResultCompanies = (IEnumerable<Company>)(await controller.GetAllCompaniesAsync() as ObjectResult).Value;
            var CompaniesWithAdditional = new List<Company>(updatedResultCompanies);

            Assert.Equal(CompaniesWithAdditional.Count, originalCompanies.Count + 1);

            var sampleCompany = CompaniesWithAdditional.FirstOrDefault(target => target.Name == "sample");
            Assert.NotNull(sampleCompany);
        }

        [Fact]
        public async void Update_company_modifies_company_to_list_async()
        {
            var options = new DbContextOptionsBuilder<CompanyServiceContext>()
                .UseInMemoryDatabase(databaseName: "Update_company_modifies_company_to_list_async")
                .Options;

            CompaniesController controller = new CompaniesController(new CompanyServiceContext(options));

            var id = Guid.NewGuid();
            var c = new Company("test", id);
            var result = controller.CreateCompanyAsync(c);

            var updatedCompany = new Company("Update", id);
            await controller.UpdateCompanyAsync(updatedCompany, id);

            var resultCompanies = (IEnumerable<Company>)(await controller.GetAllCompaniesAsync() as ObjectResult).Value;
            var updatedCompanies = new List<Company>(resultCompanies);

            var testCompany = updatedCompanies.FirstOrDefault(target => target.Name == "test");
            Assert.Null(testCompany);

            var actionResultGetCompany = (Company)(await controller.GetCompanyAsync(id) as ObjectResult).Value;
            Company resultCompany = actionResultGetCompany;
            Assert.Equal(updatedCompany.Name, resultCompany.Name);
            Assert.Equal(updatedCompany.Id, resultCompany.Id);
        }

        [Fact]
        public async void Update_non_existent_company_returns_not_found_async()
        {
            var options = new DbContextOptionsBuilder<CompanyServiceContext>()
                .UseInMemoryDatabase(databaseName: "Update_non_existent_company_returns_not_found_async")
                .Options;

            CompaniesController controller = new CompaniesController(new CompanyServiceContext(options));

            var NonExistantId = Guid.NewGuid();
            var company = new Company("Should Not Exist", NonExistantId);
            var result = await controller.UpdateCompanyAsync(company, NonExistantId);

            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public async void Delete_company_removes_from_list_async()
        {
            var testRepository = new TestMemoryRepository("Delete_company_removes_from_list_async");

            CompaniesController controller = new CompaniesController(testRepository._context);

            var deleteId = Guid.NewGuid();
            var deleteName = "Delete Me";
            var deleteCompany = new Company(deleteName, deleteId);
            await controller.CreateCompanyAsync(deleteCompany);

            var resultCompanies = (IEnumerable<Company>)(await controller.GetAllCompaniesAsync() as ObjectResult).Value;
            var companiesOriginal = new List<Company>(resultCompanies);
            var resultCompany = companiesOriginal.Find(target => target.Name == deleteName);
            Assert.NotNull(resultCompany);

            await controller.DeleteCompanyAsync(deleteId);

            var SingleDeletedResultCompanies = (IEnumerable<Company>)(await controller.GetAllCompaniesAsync() as ObjectResult).Value;
            var companiesUpdated = new List<Company>(SingleDeletedResultCompanies);
            resultCompany = companiesUpdated.Find(target => target.Name == deleteName);
            Assert.Null(resultCompany);
        }

        [Fact]
        public async void Delete_non_existent_company_returns_not_found_async()
        {
            var options = new DbContextOptionsBuilder<CompanyServiceContext>()
                .UseInMemoryDatabase(databaseName: "Delete_non_existent_company_returns_not_found_async")
                .Options;

            CompaniesController controller = new CompaniesController(new CompanyServiceContext(options));

            var nonExistentId = Guid.NewGuid();

            var result = await controller.DeleteCompanyAsync(nonExistentId);
            Assert.True(result is NotFoundResult);
        }
    }
}
