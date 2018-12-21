using System;
using System.Collections.Generic;

namespace CompanyService.Models
{
    public class Company
    {
        #region Properties
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

        public Company(string name) : this()
        {
            this.Name = name;
        }

        public Company(string name, Guid guid) : this(name)
        {
            this.Id = guid;
        }

        public override string ToString()
        {
            return this.Name;
        }
        #endregion

    }
}
