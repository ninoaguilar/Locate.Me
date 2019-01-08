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
    public class EmployeeControllerTest
    {
        [Fact]
        public async void Query_employee_list_returns_correct_employees_async()
        {
            var testRepository = new TestMemoryRepository("Query_company_list_returns_correct_employees");

            EmployeesController controller = new EmployeesController(testRepository._context);

            var result = (IEnumerable<Employee>)(await controller.GetAllEmployeesAsync(Guid.Parse("64ab7258-2669-46fa-b408-10635b1e67ae")) as ObjectResult).Value;
            var employees = new List<Employee>(result);
            Assert.Equal(2, employees.Count);
            Assert.Equal("Joeseph", employees[0].FirstName);
            Assert.Equal("Bober", employees[1].FirstName);
        }

        [Fact]
        public async void Get_employee_retrieves_company_async()
        {
            var options = new DbContextOptionsBuilder<CompanyServiceContext>()
                .UseInMemoryDatabase(databaseName: "Get_company_retrieves_company_async")
                .Options;

            EmployeesController controller = new EmployeesController(new CompanyServiceContext(options));

            string sampleFirstName = "Sample";
            string sampleLastName = "Name";
            Guid id = Guid.NewGuid();
            Employee sampleEmployee = new Employee(sampleFirstName, sampleLastName, id);

            await controller.CreateEmployeeAsync(Guid.Parse("1da8b16b-7173-4ced-94c4-5669584d8249"),sampleEmployee);

            var resultEmployee = (Employee)(await controller.GetEmployeeAsync(Guid.Parse("1da8b16b-7173-4ced-94c4-5669584d8249"),sampleEmployee.Id) as ObjectResult).Value;

            Assert.Equal(resultEmployee, sampleEmployee);
        }

        [Fact]
        public async void Get_non_existent_employee_returns_not_found_async()
        {
            var options = new DbContextOptionsBuilder<CompanyServiceContext>()
                .UseInMemoryDatabase(databaseName: "Get_non_existent_company_returns_not_foundAsync")
                .Options;

            EmployeesController controller = new EmployeesController(new CompanyServiceContext(options));

            Guid id = Guid.NewGuid();

            var resultEmployee = await controller.GetEmployeeAsync(Guid.Parse("1da8b16b-7173-4ced-94c4-5669584d8249"), id);
            var test = resultEmployee;
            Assert.True(resultEmployee is NotFoundResult);
        }

        [Fact]
        public async void Create_employee_adds_company_to_list_async()
        {
            var options = new DbContextOptionsBuilder<CompanyServiceContext>()
                .UseInMemoryDatabase(databaseName: "Create_company_adds_company_to_list_async")
                .Options;

            EmployeesController controller = new EmployeesController(new CompanyServiceContext(options));

            var resultEmployees = (IEnumerable<Employee>)(await controller.GetAllEmployeesAsync(Guid.Parse("1da8b16b-7173-4ced-94c4-5669584d8249")) as ObjectResult).Value;
            List<Employee> originalEmployees = new List<Employee>(resultEmployees);

            Employee additionalEmployee = new Employee("sample", "employee", Guid.Parse("1da8b16b-7173-4ced-94c4-5669584d8249"));
            var result = controller.CreateEmployeeAsync(Guid.Parse("1da8b16b-7173-4ced-94c4-5669584d8249"), additionalEmployee).Result;
            Assert.Equal(201, (result as ObjectResult).StatusCode);

            var updatedResultEmployees = (IEnumerable<Employee>)(await controller.GetAllEmployeesAsync(Guid.Parse("1da8b16b-7173-4ced-94c4-5669584d8249")) as ObjectResult).Value;
            var EmployeesWithAdditional = new List<Employee>(updatedResultEmployees);

            Assert.Equal(EmployeesWithAdditional.Count, originalEmployees.Count + 1);

            var sampleEmployeeFirst = EmployeesWithAdditional.FirstOrDefault(target => target.FirstName == "sample");
            var sampleEmployeeLast = EmployeesWithAdditional.FirstOrDefault(target => target.LastName == "employee");
            Assert.NotNull(sampleEmployeeFirst);
            Assert.NotNull(sampleEmployeeFirst);
        }

        [Fact]
        public async void Update_employee_modifies_employee_to_list_async()
        {
            var options = new DbContextOptionsBuilder<CompanyServiceContext>()
                .UseInMemoryDatabase(databaseName: "Update_company_modifies_company_to_list_async")
                .Options;

            EmployeesController controller = new EmployeesController(new CompanyServiceContext(options));

            var id = Guid.NewGuid();
            var c = new Employee("test", id);
            var result = controller.CreateEmployeeAsync(c);

            var updatedEmployee = new Employee("Update", id);
            await controller.UpdateEmployeeAsync(updatedEmployee, id);

            var resultEmployees = (IEnumerable<Employee>)(await controller.GetAllEmployeesAsync() as ObjectResult).Value;
            var updatedEmployees = new List<Employee>(resultEmployees);

            var testEmployee = updatedEmployees.FirstOrDefault(target => target.Name == "test");
            Assert.Null(testEmployee);

            var actionResultGetEmployee = (Employee)(await controller.GetEmployeeAsync(id) as ObjectResult).Value;
            Employee resultEmployee = actionResultGetEmployee;
            Assert.Equal(updatedEmployee.Name, resultEmployee.Name);
            Assert.Equal(updatedEmployee.Id, resultEmployee.Id);
        }

        [Fact]
        public async void Update_non_existent_employee_returns_not_found_async()
        {
            var options = new DbContextOptionsBuilder<CompanyServiceContext>()
                .UseInMemoryDatabase(databaseName: "Update_non_existent_team_returns_not_foundAsync")
                .Options;

            EmployeesController controller = new EmployeesController(new CompanyServiceContext(options));

            var NonExistantId = Guid.NewGuid();
            var company = new Employee("Should Not Exist", NonExistantId);
            var result = await controller.UpdateEmployeeAsync(company, NonExistantId);

            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public async void Delete_employee_removes_from_list_async()
        {
            var testRepository = new TestMemoryRepository("Delete_company_removes_from_list_async");

            EmployeesController controller = new EmployeesController(testRepository._context);

            var deleteId = Guid.NewGuid();
            var deleteName = "Delete Me";
            var deleteEmployee = new Employee(deleteName, deleteId);
            await controller.CreateEmployeeAsync(deleteEmployee);

            var resultEmployees = (IEnumerable<Employee>)(await controller.GetAllEmployeesAsync() as ObjectResult).Value;
            var employeesOriginal = new List<Employee>(resultEmployees);
            var resultEmployee = employeesOriginal.Find(target => target.Name == deleteName);
            Assert.NotNull(resultEmployee);

            await controller.DeleteEmployeeAsync(deleteId);

            var SingleDeletedResultEmployees = (IEnumerable<Employee>)(await controller.GetAllEmployeesAsync() as ObjectResult).Value;
            var employeesUpdated = new List<Employee>(SingleDeletedResultEmployees);
            resultEmployee = employeesUpdated.Find(target => target.Name == deleteName);
            Assert.Null(resultEmployee);
        }

        [Fact]
        public async void Delete_non_existent_employee_returns_not_found_async()
        {
            var options = new DbContextOptionsBuilder<CompanyServiceContext>()
                .UseInMemoryDatabase(databaseName: "Delete_non_existent_company_returns_not_foundAsync")
                .Options;

            EmployeesController controller = new EmployeesController(new CompanyServiceContext(options));

            var nonExistentId = Guid.NewGuid();

            var result = await controller.DeleteEmployeeAsync(nonExistentId);
            Assert.True(result is NotFoundResult);
        }
    }
}
