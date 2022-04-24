namespace Account.Domain.Model
{
    public class LedgerAccount
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Code { get; set; }
        public int Name { get; set; }
        public int VatRateId { get; set; }
        public int StatusId { get; set; }

        public ICollection<Customer> Customers { get; set; }
        public LedgerAccountCategory LedgerAccountCategory { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedUserId { get; set; }

        public Nullable<DateTime> UpdatedDate { get; set; }
        public int UpdatedUserId { get; set; }
    }
}
