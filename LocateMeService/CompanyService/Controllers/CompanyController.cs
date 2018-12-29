using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CompanyService.Models;
using CompanyService.Persistence;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CompanyService.Controllers
{
    [Route("api/[controller]")]
    public class CompanyController : Controller
    {
        ICompanyRepository _repository;

        public CompanyController(ICompanyRepository repository)
        {
            _repository = repository;
        }

        // GET: api/company
        [HttpGet]
        public virtual IActionResult GetAllCompanies()
        {
            return Ok(_repository.ListAll());
        }

        // GET api/company/5
        [HttpGet("{id}")]
        public virtual IActionResult GetCompany(Guid id)
        {
            var company = _repository.Get(id);

            if (company is null) 
            {
                return NotFound();
            }

            return Ok(company);
        }

        // POST api/company
        [HttpPost]
        public virtual IActionResult CreateCompany([FromBody]Company c)
        {
            var company = _repository.Add(c);
            return Created($"/company/{c.Id}", c);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public virtual IActionResult UpdateCompany([FromBody]Company company, Guid id)
        {
            var updatedCompany = _repository.Get(id);

            if (updatedCompany is null)
            {
                return NotFound();
            }

            _repository.Delete(id);
            _repository.Add(company);

            return Ok(company);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public virtual IActionResult DeleteCompany(Guid id)
        {
            var updatedCompany = _repository.Get(id);

            if (updatedCompany is null)
            {
                return NotFound();
            }

            _repository.Delete(id);

            return Ok(updatedCompany);
        }
    }
}
