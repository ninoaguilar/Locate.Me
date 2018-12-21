using System;

namespace CompanyService.Models
{
    public class Employee
    {
        #region Properties
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PreferredName { get; set; }
        public string Role { get; set; }
        public string Picture { get; set; }
        public DateTime DayOfBirth { get; set; }
        public string AboutSnippet { get; set; }
        public string OnlineStatus { get; set; }
        public int CompanyId { get; set; }
        #endregion

        #region Constructors
        public Employee()
        {
        }

        public Employee(Guid id) 
        : this()
        {
            this.Id = id;
        }

        public Employee(string firstName, string LastName, Guid id) 
        : this(id)
        {
            this.FirstName = firstName;
            this.LastName = LastName;
        }

        public override string ToString()
        {
            return this.FirstName + this.LastName;
        }

        #endregion

    }
}
