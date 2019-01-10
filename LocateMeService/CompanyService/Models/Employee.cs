using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyService.Models
{
    public class Employee
    {
        #region Properties
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PreferredName { get; set; }
        public string Role { get; set; }
        public string Picture { get; set; }
        public DateTime DayOfBirth { get; set; }
        public string AboutSnippet { get; set; }

public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        #endregion

        #region Constructors
        public Employee()
        {
        }
        #endregion

        public override string ToString()
        {
            return this.FirstName + this.LastName;
        }



    }
}
