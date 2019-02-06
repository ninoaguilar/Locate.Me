using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyService.Abstractions.Repository;
using CompanyService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CompanyService.Controllers
{
    //[Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly CompanyServiceContext _context;

        public EmployeesController(CompanyServiceContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/companies/{companyId}/[controller]")]
        public async Task<IActionResult> GetAllEmployeesAsync(Guid companyId)
        {
            var company = await _context.Companies.Include(u => u.Employees)
                                                  .SingleOrDefaultAsync(c => c.Id == companyId);

            return company.Employees == null ? NotFound() : (IActionResult)Ok(company.Employees);
        }

        [HttpGet]
        [Route("api/companies/{companyId}/[controller]/{employeeId}")]
        public async Task<IActionResult> GetEmployeeAsync(Guid companyId, Guid employeeId)
        {
            var employee = await _context.Employees.Where(u => u.Company.Id == companyId && u.Id == employeeId)
                                                   .SingleOrDefaultAsync();
                
            return employee == null ? NotFound() : (IActionResult)Ok(employee);
        }

        /*
        [HttpGet]
        [Route("/companies/{employeeId}/company")]
        public async Task<IActionResult> GetCompanyForEmployeeAsync(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public Guid GetCompanyForEmployeeAsync(Guid employeeId)
        {
            throw new NotImplementedException();
        }
        */

        [HttpPost]
        [Route("api/companies/{companyId}/[controller]")]
        public async Task<IActionResult> CreateEmployeeAsync(Guid companyId, [FromBody]Employee employee)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(u => u.Id == companyId);

            if (company == null)
            {
                return NotFound();
            }

            company.Employees.Add(employee);

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return (IActionResult)Created($"api/companies/{companyId}/employees/{employee.Id}/", employee);
        }

        [HttpPut]
        [Route("api/companies/{companyId}/[controller]/{employeeId}")]
        public async Task<IActionResult> UpdateEmployeeAsync(Guid companyId, [FromBody]Employee employee)
        {
            var updateEmployee = await _context.Employees.Where(u => u.Company.Id == companyId && u.Id == employee.Id)
                                                              .SingleOrDefaultAsync();

            if (updateEmployee == null)
            {
                return NotFound();
            }

            updateEmployee.FirstName = employee.FirstName;
            updateEmployee.LastName = employee.LastName;

            await _context.SaveChangesAsync();

            return (IActionResult)Ok(updateEmployee);
        }

        // DELETE api/values/5
        [HttpDelete]
        [Route("api/companies/{companyId}/[controller]/{employeeId}")]
        public async Task<IActionResult> DeleteEmployeeAsync(Guid companyId, Guid employeeId)
        {
            var _company = await _context.Companies.SingleOrDefaultAsync(c => c.Id == companyId);

            if (_company == null)
            {
                return NotFound();
            }

            var employeeToDelete = new Employee();
            foreach (var _employee in _company.Employees.ToList())
            {
                if (_employee.Id == employeeId)
                {
                    employeeToDelete = _employee;
                    _company.Employees.Remove(_employee);
                    _context.Employees.Remove(_employee);
                }
            }

            if (employeeToDelete.Id == Guid.Empty)
            {
                return NotFound();
            }

            await _context.SaveChangesAsync();
            return (IActionResult)Ok(employeeToDelete);
        }
    }
}
