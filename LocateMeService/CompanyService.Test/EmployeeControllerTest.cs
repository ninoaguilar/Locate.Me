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
            var testRepository = new TestMemoryRepository("Query_employee_list_returns_correct_employees_async");
            EmployeesController controller = new EmployeesController(testRepository._context);

            var companyId = Guid.Parse("86c06754-d473-4e77-9350-d61ba8cf190b");

            var result = await controller.CreateEmployeeAsync(companyId, new Employee { FirstName = "Nino", LastName = "Aguilar" }) as ObjectResult;
            Assert.Equal(201, (result).StatusCode);
            result = await controller.CreateEmployeeAsync(companyId, new Employee { FirstName = "Test", LastName = "Employee" }) as ObjectResult;
            Assert.Equal(201, (result).StatusCode);

            var allEmployeeResult = (IEnumerable<Employee>)(await controller.GetAllEmployeesAsync(companyId) as ObjectResult).Value;
            var employees = new List<Employee>(allEmployeeResult);
            Assert.Equal(2, employees.Count);
            Assert.Equal("Nino", employees[0].FirstName);
            Assert.Equal("Aguilar", employees[0].LastName);
            Assert.Equal("Test", employees[1].FirstName);
            Assert.Equal("Employee", employees[1].LastName);
        }

        [Fact]
        public async void Get_employee_retrieves_employee_async()
        {
            var testRepository = new TestMemoryRepository("Get_employee_retrieves_employee_async");
            EmployeesController controller = new EmployeesController(testRepository._context);

            var companyId = Guid.Parse("86c06754-d473-4e77-9350-d61ba8cf190b");

            var sampleEmployee = new Employee { FirstName = "Sample", LastName = "Name" };

            var result = await controller.CreateEmployeeAsync(companyId, sampleEmployee) as ObjectResult;
            Assert.Equal(201, result.StatusCode);

            var recentlyCreatedEmployee = (Employee)result.Value;
            var resultEmployee = (Employee)(await controller.GetEmployeeAsync(companyId, recentlyCreatedEmployee.Id) as ObjectResult).Value;

            Assert.Equal(resultEmployee.FirstName, sampleEmployee.FirstName);
            Assert.Equal(resultEmployee.LastName, sampleEmployee.LastName);
        }

        [Fact]
        public async void Get_non_existent_employee_returns_not_found_async()
        {
            var testRepository = new TestMemoryRepository("Get_non_existent_employee_returns_not_found_async");
            EmployeesController controller = new EmployeesController(testRepository._context);

            var companyId = Guid.Parse("86c06754-d473-4e77-9350-d61ba8cf190b");

            Guid id = Guid.NewGuid();

            var resultEmployee = await controller.GetEmployeeAsync(companyId, id);
            var test = resultEmployee;
            Assert.True(resultEmployee is NotFoundResult);
        }

        [Fact]
        public async void Create_employee_adds_employee_to_company_list_async()
        {
            var testRepository = new TestMemoryRepository("Create_employee_adds_employee_to_company_list_async");
            EmployeesController controller = new EmployeesController(testRepository._context);

            var companyId = Guid.Parse("86c06754-d473-4e77-9350-d61ba8cf190b");

            var resultEmployees = (IEnumerable<Employee>)(await controller.GetAllEmployeesAsync(companyId) as ObjectResult).Value;
            var originalEmployees = new List<Employee>(resultEmployees);

            var result = await controller.CreateEmployeeAsync(companyId, new Employee { FirstName = "sample", LastName = "employee" }) as ObjectResult;
            Assert.Equal(201, (result).StatusCode);

            var updatedResultEmployees = (IEnumerable<Employee>)(await controller.GetAllEmployeesAsync(companyId) as ObjectResult).Value;
            var EmployeesWithAdditional = new List<Employee>(updatedResultEmployees);

            Assert.Equal(EmployeesWithAdditional.Count, originalEmployees.Count + 1);

            var sampleEmployeeFirst = EmployeesWithAdditional.FirstOrDefault(target => target.FirstName == "sample");
            var sampleEmployeeLast = EmployeesWithAdditional.FirstOrDefault(target => target.LastName == "employee");
            var sampleEmployeeCompanyId = EmployeesWithAdditional.FirstOrDefault(target => target.CompanyId == companyId);
            Assert.NotNull(sampleEmployeeFirst);
            Assert.NotNull(sampleEmployeeFirst);
            Assert.NotNull(sampleEmployeeCompanyId);
        }

        [Fact]
        public async void Update_employee_modifies_employee_to_list_async()
        {
            var testRepository = new TestMemoryRepository("Update_employee_modifies_employee_to_list_async");
            EmployeesController controller = new EmployeesController(testRepository._context);

            var companyId = Guid.Parse("86c06754-d473-4e77-9350-d61ba8cf190b");

            var employee = new Employee
            {
                FirstName = "test",
                LastName = "employee"
            };
            var result = await controller.CreateEmployeeAsync(companyId, employee) as ObjectResult;
            Assert.Equal(201, (result).StatusCode);
            var resultEmployee = (Employee)result.Value;

            var updatedEmployee = new Employee
            {
                Id = resultEmployee.Id,
                FirstName = "Update",
                LastName = "employee"
            };
            await controller.UpdateEmployeeAsync(companyId, updatedEmployee);

            var resultEmployees = (IEnumerable<Employee>)(await controller.GetAllEmployeesAsync(companyId) as ObjectResult).Value;
            var updatedEmployees = new List<Employee>(resultEmployees);

            var testEmployee = updatedEmployees.FirstOrDefault(target => target.FirstName == "Update");
            Assert.NotNull(testEmployee);

            var actionResultGetEmployee = (Employee)(await controller.GetEmployeeAsync(companyId, testEmployee.Id) as ObjectResult).Value;
            Employee resultEmployeeUpdated = actionResultGetEmployee;
            Assert.Equal(updatedEmployee.FirstName, resultEmployeeUpdated.FirstName);
            Assert.Equal(updatedEmployee.Id, resultEmployeeUpdated.Id);
        }

        [Fact]
        public async void Update_non_existent_employee_returns_not_found_async()
        {
            var testRepository = new TestMemoryRepository("Update_non_existent_employee_returns_not_found_async");
            EmployeesController controller = new EmployeesController(testRepository._context);

            var companyId = Guid.Parse("86c06754-d473-4e77-9350-d61ba8cf190b");

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = "Should Not",
                LastName = "Exist"
            };

            var result = await controller.UpdateEmployeeAsync(companyId, employee);

            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public async void Delete_employee_removes_from_list_async()
        {
            var testRepository = new TestMemoryRepository("Delete_employee_removes_from_list_async");
            EmployeesController controller = new EmployeesController(testRepository._context);

            var companyId = Guid.Parse("86c06754-d473-4e77-9350-d61ba8cf190b");

            var deleteFirstName = "Delete";
            var deleteLastName = "Me";
            var deleteEmployee = new Employee
            {
                FirstName = deleteFirstName,
                LastName = deleteLastName
            };

            var result = await controller.CreateEmployeeAsync(companyId, deleteEmployee) as ObjectResult;
            Assert.Equal(201, (result).StatusCode);
            var createdEmployee = (Employee)result.Value;

            var resultEmployees = (IEnumerable<Employee>)(await controller.GetAllEmployeesAsync(companyId) as ObjectResult).Value;
            var employeesOriginal = new List<Employee>(resultEmployees);
            var resultEmployee = employeesOriginal.Find(target => target.FirstName == deleteFirstName);
            Assert.NotNull(resultEmployee);

            await controller.DeleteEmployeeAsync(companyId, createdEmployee.Id);

            var SingleDeletedResultEmployees = (IEnumerable<Employee>)(await controller.GetAllEmployeesAsync(companyId) as ObjectResult).Value;
            var employeesUpdated = new List<Employee>(SingleDeletedResultEmployees);
            resultEmployee = employeesUpdated.Find(target => target.FirstName == deleteFirstName);
            Assert.Null(resultEmployee);
        }

        [Fact]
        public async void Delete_non_existent_employee_returns_not_found_async()
        {
            var testRepository = new TestMemoryRepository("Delete_non_existent_employee_returns_not_found_async");
            EmployeesController controller = new EmployeesController(testRepository._context);

            var companyId = Guid.Parse("86c06754-d473-4e77-9350-d61ba8cf190b");
            var nonExistentId = Guid.NewGuid();

            var result = await controller.DeleteEmployeeAsync(companyId, nonExistentId);
            Assert.True(result is NotFoundResult);
        }
    }
}
