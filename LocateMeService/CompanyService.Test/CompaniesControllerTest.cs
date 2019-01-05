using System;
using System.Collections.Generic;
using Xunit;
using CompanyService.Models;
using CompanyService.Controllers;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace CompanyService.Test
{
    public class CompanyControllerTest
    {
        [Fact]
        public void QueryCompanyListReturnsCorrectCompanies()
        {
            EmployeesController controller = new EmployeesController(new TestMemoryRepository());

            var actionResultGetAll = controller.GetAllEmployees() as ObjectResult;
            var rawCompanies = (IEnumerable<Company>)actionResultGetAll.Value;
            List<Company> companies = new List<Company>(rawCompanies);
            Assert.Equal(2, companies.Count);
            Assert.Equal("Test Company One", companies[0].Name);
            Assert.Equal("Test Company Two", companies[1].Name);
        }

        [Fact]
        public void GetCompanyRetrievesCompany()
        {
            EmployeesController controller = new EmployeesController(new TestMemoryRepository());

            string testName = "Test Company";
            Guid id = Guid.NewGuid();
            Company testCompany = new Company(testName, id);

            controller.CreateCompany(testCompany);
            var actionResultGetCompany = controller.GetCompany(testCompany.Id) as ObjectResult;
            Company result = (Company)actionResultGetCompany.Value;

            Assert.Equal(result, testCompany);
        }

        [Fact]
        public void GetNonExistentCompanyReturnsNotFound()
        {
            EmployeesController controller = new EmployeesController(new TestMemoryRepository());

            Guid id = Guid.NewGuid();

            var result = controller.GetCompany(id);

            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public void CreateCompanyAddsCompanyToList()
        {
            EmployeesController controller = new EmployeesController(new TestMemoryRepository());

            var actionResultGetAll = controller.GetAllCompanies() as ObjectResult;
            List<Company> originalCompanies = new List<Company>((IEnumerable<Company>)actionResultGetAll.Value);

            Company additionalCompany = new Company("sample");
            var result = controller.CreateCompany(additionalCompany);
            Assert.Equal(201, (result as ObjectResult).StatusCode);

            var actionResultAdditional = controller.GetAllCompanies() as ObjectResult;
            var CompaniesWithAdditional = new List<Company>((IEnumerable<Company>)(actionResultAdditional).Value);

            Assert.Equal(CompaniesWithAdditional.Count, originalCompanies.Count + 1);

            var sampleCompany = CompaniesWithAdditional.FirstOrDefault(target => target.Name == "sample");
            Assert.NotNull(sampleCompany);
        }

        [Fact]
        public void UpdateCompanyModifiesCompanyToList()
        {
            EmployeesController controller = new EmployeesController(new TestMemoryRepository());

            var id = Guid.NewGuid();
            var c = new Company("test", id);
            var result = controller.CreateCompany(c);

            var updatedCompany = new Company("Update", id);
            controller.UpdateCompany(updatedCompany, id);

            var actionResultUpdatedGetAll = controller.GetAllCompanies() as ObjectResult;
            var updatedCompanies = new List<Company>((IEnumerable<Company>)actionResultUpdatedGetAll.Value);
            var testCompany = updatedCompanies.FirstOrDefault(target => target.Name == "test");
            Assert.Null(testCompany);

            var actionResultGetCompany = controller.GetCompany(id) as ObjectResult;
            Company resultCompany = (Company)actionResultGetCompany.Value;
            Assert.Equal(updatedCompany, resultCompany);
        }

        [Fact]
        public void UpdateNonExistentTeamReturnsNotFound()
        {
            EmployeesController controller = new EmployeesController(new TestMemoryRepository());

            var NonExistantId = Guid.NewGuid();
            var company = new Company("Should Not Exist", NonExistantId);
            var result = controller.UpdateCompany(company, NonExistantId);

            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public void DeleteCompanyRemovesFromList()
        {
            EmployeesController controller = new EmployeesController(new TestMemoryRepository());

            var deleteId = Guid.NewGuid();
            var deleteName = "Delete Me";
            var deleteCompany = new Company(deleteName, deleteId);
            controller.CreateCompany(deleteCompany);

            var actionResultOriginal = controller.GetAllCompanies() as ObjectResult;
            var resultCompanies = new List<Company>((IEnumerable<Company>)actionResultOriginal.Value);
            var resultCompany = resultCompanies.Find(target => target.Name == deleteName);
            Assert.NotNull(resultCompany);

            controller.DeleteCompany(deleteId);

            var actionResultDeleted = controller.GetAllCompanies() as ObjectResult;
            resultCompanies = new List<Company>((IEnumerable<Company>)actionResultDeleted.Value);
            resultCompany = resultCompanies.Find(target => target.Name == deleteName);
            Assert.Null(resultCompany);
        }

        [Fact]
        public void DeleteNonExistentCompanyReturnsNotFound()
        {
            EmployeesController controller = new EmployeesController(new TestMemoryRepository());

            var nonExistentId = Guid.NewGuid();

            var result = controller.DeleteCompany(nonExistentId);
            Assert.True(result is NotFoundResult);
        }
    }
}
