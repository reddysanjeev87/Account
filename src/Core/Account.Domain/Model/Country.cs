namespace Account.Domain.Model
{
    public class Country
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public ICollection<Customer> InvoiceCustomers { get; set; }
        public ICollection<Customer> DeliveryCustomers { get; set; }

    }
}
