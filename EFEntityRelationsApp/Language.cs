using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFEntityRelationsApp
{
    //[ComplexType]
    public class Language
    {
        public string Title { get; set; } = null!;
    }
}
