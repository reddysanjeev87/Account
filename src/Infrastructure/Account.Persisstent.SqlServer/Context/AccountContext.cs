
using Account.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Account.Persisstent.SqlServer
{
    public partial class AccountContext : DbContext
    {
        public DbSet<Status> Statuses { get; set; }
        public DbSet<VatRate> VatRates { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<PriceType> PriceTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<LedgerAccount> LedgerAccounts { get; set; }
        public DbSet<LedgerAccountCategory> LedgerAccountCategories { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Country> Countries { get; set; }
        public AccountContext()
        {
        }

        public AccountContext(DbContextOptions<AccountContext> options)
            : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status", "dbo");
                entity.HasKey(t => t.Id);
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<VatRate>(entity =>
            {
                entity.ToTable("VatRate", "dbo");
                entity.HasKey(t => t.Id);
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.HasOne(d => d.Status).WithMany(p => p.VatRates).HasForeignKey
                (t => t.StatusId).OnDelete(DeleteBehavior.ClientSetNull);

            });
            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("State", "dbo");
                entity.HasKey(t => t.Id);
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("Region", "dbo");
                entity.HasKey(t => t.Id);
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

            });
            modelBuilder.Entity<Province>(entity =>
            {
                entity.ToTable("Province", "dbo");
                entity.HasKey(t => t.Id);
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country", "dbo");
                entity.HasKey(t => t.Id);
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired()
                    .IsUnicode(false);
            });
            modelBuilder.Entity<PriceType>(entity =>
            {
                entity.ToTable("PriceType", "dbo");
                entity.HasKey(t => t.Id);
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired()
                    .IsUnicode(false);
                entity.HasOne(d => d.Status).WithMany(p => p.PriceTypes).HasForeignKey
               (t => t.StatusId).OnDelete(DeleteBehavior.ClientSetNull);
            });
            modelBuilder.Entity<LedgerAccountCategory>(entity =>
            {
                entity.ToTable("LedgerAccountCategory", "dbo");
                entity.HasKey(t => t.Id);
                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsRequired()
                    .IsUnicode(false);
                entity.HasOne(d => d.Status).WithMany(p => p.LedgerAccountCategories).HasForeignKey
               (t => t.StatusId).OnDelete(DeleteBehavior.ClientSetNull);
            });
            modelBuilder.Entity<LedgerAccount>(entity =>
            {
                entity.ToTable("LedgerAccount", "dbo");
                entity.HasKey(t => t.Id);
                entity.Property(e => e.Code)
                   .HasMaxLength(10)
                   .IsRequired()
                   .IsUnicode(false);
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired()
                    .IsUnicode(false);
                entity.HasOne(d => d.LedgerAccountCategory).WithMany(p => p.LedgerAccounts).HasForeignKey
              (t => t.StatusId).OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(d => d.Status).WithMany(p => p.LedgerAccounts).HasForeignKey
               (t => t.StatusId).OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer", "dbo");

                entity.HasKey(t => t.Id);

                entity.Property(e => e.BusinessName)
                  .HasMaxLength(250)
                  .IsRequired()
                  .IsUnicode(false);

                entity.Property(e => e.ContactName)
                 .HasMaxLength(250)
                 .IsRequired(false)
                 .IsUnicode(false);

                entity.Property(e => e.Email)
                 .HasMaxLength(50)
                 .IsRequired(false)
                 .IsUnicode(false);

                entity.Property(e => e.Mobile)
               .HasMaxLength(50)
               .IsRequired(false)
               .IsUnicode(false);

                entity.Property(e => e.Reference)
                 .HasMaxLength(250)
                 .IsRequired(false)
                 .IsUnicode(false);

                entity.Property(e => e.Telephone)
                .HasMaxLength(50)
                .IsRequired(false)
                .IsUnicode(false);

                entity.Property(e => e.InvoiceAdress1)
                .HasMaxLength(500)
                .IsRequired(false)
                .IsUnicode(false);

                entity.Property(e => e.InvoiceAdress2)
               .HasMaxLength(500)
               .IsRequired(false)
               .IsUnicode(false);

                entity.Property(e => e.InvoiceCity)
              .HasMaxLength(250)
              .IsRequired(false)
              .IsUnicode(false);

                entity.Property(e => e.InvoiceCounty)
             .HasMaxLength(250)
             .IsRequired(false)
             .IsUnicode(false);

                entity.Property(e => e.InvoicePostCode)
             .HasMaxLength(20)
             .IsRequired(false)
             .IsUnicode(false);

                entity.Property(e => e.InvoiceVatNumber)
           .HasMaxLength(100)
           .IsRequired(false)
           .IsUnicode(false);

                entity.Property(e => e.DeliveryAdress1)
                .HasMaxLength(500)
                .IsRequired(false)
                .IsUnicode(false);

                entity.Property(e => e.DeliveryAdress2)
               .HasMaxLength(500)
               .IsRequired(false)
               .IsUnicode(false);

                entity.Property(e => e.DeliveryCity)
              .HasMaxLength(250)
              .IsRequired(false)
              .IsUnicode(false);

                entity.Property(e => e.DeliveryCounty)
             .HasMaxLength(250)
             .IsRequired(false)
             .IsUnicode(false);

                entity.Property(e => e.DeliveryPostCode)
             .HasMaxLength(20)
             .IsRequired(false)
             .IsUnicode(false);

                entity.Property(e => e.BankAccountName)
            .HasMaxLength(100)
            .IsRequired(false)
            .IsUnicode(false);

                entity.Property(e => e.BankAccountNumber)
            .HasMaxLength(100)
            .IsRequired(false)
            .IsUnicode(false);

                entity.Property(e => e.BankAccountSortCode)
           .HasMaxLength(10)
           .IsRequired(false)
           .IsUnicode(false);
                entity.Property(e => e.BankAccountSwiftCode)
           .HasMaxLength(100)
           .IsRequired(false)
           .IsUnicode(false);

                entity.Property(e => e.BankAccountIBAN)
          .HasMaxLength(100)
          .IsRequired(false)
          .IsUnicode(false);



                entity.HasOne(d => d.InvoiceRegion).WithMany(p => p.InvoiceCustomers).HasForeignKey
              (t => t.InvoiceRegionId).OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.InvoiceState).WithMany(p => p.InvoiceCustomers).HasForeignKey
            (t => t.InvoiceStateId).OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.InvoiceProvince).WithMany(p => p.InvoiceCustomers).HasForeignKey
            (t => t.InvoiceProvinceId).OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.InvoiceLedgerAccount).WithMany(p => p.Customers).HasForeignKey
               (t => t.InvoiceLedgerAccountId).OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.InvoiceCountry).WithMany(p => p.InvoiceCustomers).HasForeignKey
            (t => t.InvoiceCountryId).OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.DeliveryRegion).WithMany(p => p.DeliveryCustomers).HasForeignKey
             (t => t.DeliveryRegionId).OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.DeliveryState).WithMany(p => p.DeliveryCustomers).HasForeignKey
            (t => t.DeliveryStateId).OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.DeliveryProvince).WithMany(p => p.DeliveryCustomers).HasForeignKey
            (t => t.DeliveryProvinceId).OnDelete(DeleteBehavior.ClientSetNull);

                // entity.HasOne(d => d.DeliveryLedgerAccount).WithMany(p => p.Customers).HasForeignKey
                //(t => t.DeliveryLedgerAccountId).OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.DeliveryCountry).WithMany(p => p.DeliveryCustomers).HasForeignKey
            (t => t.DeliveryCountryId).OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.PriceType).WithMany(p => p.Customers).HasForeignKey
               (t => t.CustomerPriceTypeId).OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Status).WithMany(p => p.Customers).HasForeignKey
               (t => t.StatusId).OnDelete(DeleteBehavior.ClientSetNull);
            });


        }


    }

}
