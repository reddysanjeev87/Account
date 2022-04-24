namespace Account.Domain.Model
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<VatRate> VatRates { get; set; }
        public ICollection<PriceType> PriceTypes { get; set; }
        public ICollection<LedgerAccountCategory> LedgerAccountCategories { get; set; }
        public ICollection<LedgerAccount> LedgerAccounts { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }
}
