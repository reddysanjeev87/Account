using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Model
{
    public class Province
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Customer> InvoiceCustomers { get; set; }
        public ICollection<Customer> DeliveryCustomers { get; set; }
    }
}
