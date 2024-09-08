using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFEntityRelationsApp
{
    //[Owned]
    public class Passport
    {
        public int Id { get; set; }
        public string Series { get; set; } = null!;
        public string Number { get; set; } = null!;

        //public int EmployeeId { get; set; }
        //public Employee? Employee { get; set; }
    }
}
