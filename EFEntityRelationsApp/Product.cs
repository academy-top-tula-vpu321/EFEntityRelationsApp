using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFEntityRelationsApp
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int? ParentId { get; set; }
        public Product? Parent { get; set; }
        public List<Product> ChildsProducts { get; set; } = new();
    }
}
