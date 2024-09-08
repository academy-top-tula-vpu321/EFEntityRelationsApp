using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EFEntityRelationsApp
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime BirthDate { get; set; }

        public int? CompanyId { get; set; }
        public Company? Company { get; set; } = null!;

        public Position? Position {  get; set; }
        public Passport? Passport { get; set; }

        public List<Project>? Projects { get; set; } = new();

        public List<EmployeeProject> EmployeeProjects { get; set; } = new();
    }
}
