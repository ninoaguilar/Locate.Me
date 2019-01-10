using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace CompanyService.Models
{
    public class Company
    {
        #region Properties
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public ICollection<Employee> Employees { get; set; }
        #endregion

        #region Constructors
        public Company()
        {
            this.Employees = new List<Employee>();
        }

        #endregion

        public override string ToString()
        {
            return this.Name;
        }


    }
}
