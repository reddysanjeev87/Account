using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Model
{
    public class PriceType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }
}
