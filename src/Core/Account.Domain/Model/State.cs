namespace Account.Domain.Model
{
    public class State
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ICollection<Customer> InvoiceCustomers { get; set; }
        public ICollection<Customer> DeliveryCustomers { get; set; }
    }
}
