using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinqStudies.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public List<Product> Products { get; set; }
        public decimal Total { get; set; }
    }
}
