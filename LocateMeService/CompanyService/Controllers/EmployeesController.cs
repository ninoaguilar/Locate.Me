using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyService.Models;
using CompanyService.Persistence;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CompanyService.Controllers
{
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        readonly ICompanyRepository _repository;

        public EmployeesController(ICompanyRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("/companies/{companyId}/[controller]")]
        public IEnumerable<string> GetEmployees(Guid companyId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("/companies/{companyId}/[controller]/{employeeId}")]
        public IEnumerable<string> GetEmployee(Guid companyId, Guid employeeId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("/companies/{employeeId}/team")]
        public IActionResult GetTeamForMember(Guid memberId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("/companies/{teamId}/[controller]/{memberId}")]
        public void CreateEmployee([FromBody]Employee employee, Guid companyId, Guid employeeId)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("/companies/{teamId}/[controller]/{memberId}")]
        public void UpdateEmployee([FromBody]Employee employee, Guid companyId, Guid employeeId)
        {
            throw new NotImplementedException();
        }

        // DELETE api/values/5
        [HttpDelete]
        [Route("/companies/{teamId}/[controller]/{memberId}")]
        public void DeleteEmployee([FromBody]Employee employee, Guid companyId, Guid employeeId)
        {
        }
    }
}
