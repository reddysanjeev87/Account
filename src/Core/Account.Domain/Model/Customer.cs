using System.ComponentModel.DataAnnotations;

namespace Account.Domain.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string BusinessName { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Reference { get; set; }
        public string Telephone { get; set; }
        public Nullable<int> InvoiceRegionId { get; set; }
        public string InvoiceAdress1 { get; set; }
        public string InvoiceAdress2 { get; set; }
        public string InvoiceCity { get; set; }
        public Nullable<int> InvoiceProvinceId { get; set; }
        public Nullable<int> InvoiceStateId { get; set; }
        public string InvoiceCounty { get; set; }
        public string InvoicePostCode { get; set; }
        public Nullable<int> InvoiceCountryId { get; set; }
        public Nullable<int> InvoiceLedgerAccountId { get; set; }
        public string InvoiceVatNumber { get; set; }
        public Nullable<bool> IsSameAsInvoiceAddress { get; set; }

        public Nullable<int> DeliveryRegionId { get; set; }
        public string DeliveryAdress1 { get; set; }
        public string DeliveryAdress2 { get; set; }

        public Nullable<int> DeliveryProvinceId { get; set; }
        public Nullable<int> DeliveryStateId { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryCounty { get; set; }
        public string DeliveryPostCode { get; set; }
        public int DeliveryCountryId { get; set; }

        public Nullable<bool> IsCreditLimitEnabled { get; set; }
        public Nullable<decimal> CreditLimit { get; set; }

        public bool IsCreditTermEnabled { get; set; }
        public string CreditTerm { get; set; }
        public string BankAccountName { get; set; }
        public string BankAccountSortCode { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankAccountSwiftCode { get; set; }
        public string BankAccountIBAN { get; set; }

        public Nullable<int> CustomerPriceTypeId { get; set; }

        public string Notes { get; set; }

        public int StatusId { get; set; }

        public Region InvoiceRegion { get; set; }
        public Province InvoiceProvince { get; set; }
        public State InvoiceState { get; set; }
        public Country InvoiceCountry { get; set; }
        public LedgerAccount InvoiceLedgerAccount { get; set; }

        public Region DeliveryRegion { get; set; }
        public Province DeliveryProvince { get; set; }
        public State DeliveryState { get; set; }
        public Country DeliveryCountry { get; set; }
        //public LedgerAccount DeliveryLedgerAccount { get; set; }

        public PriceType PriceType { get; set; }
        public Status Status { get; set; }

        public DateTime CreatedDate { get; set; }
        public int CreatedUserId { get; set; }

        public Nullable<DateTime> UpdatedDate { get; set; }
        public int UpdatedUserId { get; set; }
    }
}
