using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyService.Abstractions.Repository;
using CompanyService.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CompanyService.Controllers
{
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly CompanyServiceContext _context;

        public EmployeesController(CompanyServiceContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/companies/{companyId}/[controller]")]
        public async Task<IActionResult> GetAllEmployeesAsync(Guid companyId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("/companies/{companyId}/[controller]/{employeeId}")]
        public async Task<IActionResult> GetEmployeeAsync(Guid companyId, Guid employeeId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("/companies/{employeeId}/company")]
        public async Task<IActionResult> GetCompanyForEmployeeAsync(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("/companies/{companyId}/[controller]/{employeeId}")]
        public async Task<IActionResult> CreateEmployeeAsync(Guid companyId, [FromBody]Employee employee)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("/companies/{companyId}/[controller]/{employeeId}")]
        public async Task<IActionResult> UpdateEmployeeAsync(Guid companyId, [FromBody]Employee employee)
        {
            throw new NotImplementedException();
        }

        // DELETE api/values/5
        [HttpDelete]
        [Route("/companies/{companyId}/[controller]/{employeeId}")]
        public async Task<IActionResult> DeleteEmployeeAsync(Guid companyId, [FromBody]Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
