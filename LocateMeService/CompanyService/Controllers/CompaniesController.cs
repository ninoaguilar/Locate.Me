using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CompanyService.Models;
using CompanyService.Abstractions.Repository;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CompanyService.Controllers
{
    [Route("api/[controller]")]
    public class CompaniesController : Controller
    {
        private readonly CompanyServiceContext _context;

        public CompaniesController(CompanyServiceContext context)
        {
            _context = context;
        }

        // GET: api/companies
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllCompaniesAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companies = await _context.Companies.ToListAsync();

            return companies == null ? NotFound() : (IActionResult)Ok(companies);
        }

        // GET api/company/5
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetCompanyAsync(Guid id)
        {
            var company = await _context.Companies.SingleOrDefaultAsync(c => c.Id == id);
                
            return company == null ? NotFound() : (IActionResult)Ok(company);
        }

        // POST api/company
        [HttpPost]
        public virtual async Task<IActionResult> CreateCompanyAsync([FromBody]Company c)
        {
            await _context.AddAsync(c);
            await _context.SaveChangesAsync();

            return (IActionResult)Created($"/company/{c.Id}", c);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> UpdateCompanyAsync([FromBody]Company c, Guid id)
        {
            Company company = await _context.Companies.SingleOrDefaultAsync(u => u.Id == id);

            if (c.Id != id || company == null) { return NotFound(); }
             
            company.Address = c.Address;
            company.Name = c.Name;
            company.PhoneNumber = c.PhoneNumber;

            await _context.SaveChangesAsync();

            return Ok(company);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteCompanyAsync(Guid id)
        {
            Company company = await _context.Companies.SingleOrDefaultAsync(u => u.Id == id);

            if (company == null)
            {
                return NotFound();
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return Ok(company);
        }
    }
}
