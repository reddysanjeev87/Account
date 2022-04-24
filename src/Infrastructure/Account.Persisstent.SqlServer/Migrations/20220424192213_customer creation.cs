using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Account.Persisstent.SqlServer.Migrations
{
    public partial class customercreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LedgerAccountCategory",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerAccountCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LedgerAccountCategory_Status_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "dbo",
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PriceType",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceType_Status_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "dbo",
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Province",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "State",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LedgerAccount",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Name = table.Column<int>(type: "int", unicode: false, maxLength: 100, nullable: false),
                    VatRateId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LedgerAccount_LedgerAccountCategory_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "dbo",
                        principalTable: "LedgerAccountCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LedgerAccount_Status_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "dbo",
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessName = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    ContactName = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Mobile = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Reference = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    Telephone = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    InvoiceRegionId = table.Column<int>(type: "int", nullable: false),
                    InvoiceAdress1 = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    InvoiceAdress2 = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    InvoiceCity = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    InvoiceProvinceId = table.Column<int>(type: "int", nullable: false),
                    InvoiceStateId = table.Column<int>(type: "int", nullable: false),
                    InvoiceCounty = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    InvoicePostCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    InvoiceCountryId = table.Column<int>(type: "int", nullable: false),
                    InvoiceLedgerAccountId = table.Column<int>(type: "int", nullable: false),
                    InvoiceVatNumber = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    IsSameAsInvoiceAddress = table.Column<bool>(type: "bit", nullable: true),
                    DeliveryRegionId = table.Column<int>(type: "int", nullable: false),
                    DeliveryAdress1 = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    DeliveryAdress2 = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    DeliveryProvinceId = table.Column<int>(type: "int", nullable: false),
                    DeliveryStateId = table.Column<int>(type: "int", nullable: false),
                    DeliveryCity = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    DeliveryCounty = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    DeliveryPostCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    DeliveryCountryId = table.Column<int>(type: "int", nullable: false),
                    IsCreditLimitEnabled = table.Column<bool>(type: "bit", nullable: true),
                    CreditLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsCreditTermEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreditTerm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankAccountName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    BankAccountSortCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    BankAccountNumber = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    BankAccountSwiftCode = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    BankAccountIBAN = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CustomerPriceTypeId = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_Country_DeliveryCountryId",
                        column: x => x.DeliveryCountryId,
                        principalSchema: "dbo",
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customer_Country_InvoiceCountryId",
                        column: x => x.InvoiceCountryId,
                        principalSchema: "dbo",
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customer_LedgerAccount_InvoiceLedgerAccountId",
                        column: x => x.InvoiceLedgerAccountId,
                        principalSchema: "dbo",
                        principalTable: "LedgerAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customer_PriceType_CustomerPriceTypeId",
                        column: x => x.CustomerPriceTypeId,
                        principalSchema: "dbo",
                        principalTable: "PriceType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customer_Province_DeliveryProvinceId",
                        column: x => x.DeliveryProvinceId,
                        principalSchema: "dbo",
                        principalTable: "Province",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customer_Province_InvoiceProvinceId",
                        column: x => x.InvoiceProvinceId,
                        principalSchema: "dbo",
                        principalTable: "Province",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customer_Region_DeliveryRegionId",
                        column: x => x.DeliveryRegionId,
                        principalSchema: "dbo",
                        principalTable: "Region",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customer_Region_InvoiceRegionId",
                        column: x => x.InvoiceRegionId,
                        principalSchema: "dbo",
                        principalTable: "Region",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customer_State_DeliveryStateId",
                        column: x => x.DeliveryStateId,
                        principalSchema: "dbo",
                        principalTable: "State",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customer_State_InvoiceStateId",
                        column: x => x.InvoiceStateId,
                        principalSchema: "dbo",
                        principalTable: "State",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customer_Status_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "dbo",
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CustomerPriceTypeId",
                schema: "dbo",
                table: "Customer",
                column: "CustomerPriceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_DeliveryCountryId",
                schema: "dbo",
                table: "Customer",
                column: "DeliveryCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_DeliveryProvinceId",
                schema: "dbo",
                table: "Customer",
                column: "DeliveryProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_DeliveryRegionId",
                schema: "dbo",
                table: "Customer",
                column: "DeliveryRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_DeliveryStateId",
                schema: "dbo",
                table: "Customer",
                column: "DeliveryStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_InvoiceCountryId",
                schema: "dbo",
                table: "Customer",
                column: "InvoiceCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_InvoiceLedgerAccountId",
                schema: "dbo",
                table: "Customer",
                column: "InvoiceLedgerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_InvoiceProvinceId",
                schema: "dbo",
                table: "Customer",
                column: "InvoiceProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_InvoiceRegionId",
                schema: "dbo",
                table: "Customer",
                column: "InvoiceRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_InvoiceStateId",
                schema: "dbo",
                table: "Customer",
                column: "InvoiceStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_StatusId",
                schema: "dbo",
                table: "Customer",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerAccount_StatusId",
                schema: "dbo",
                table: "LedgerAccount",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerAccountCategory_StatusId",
                schema: "dbo",
                table: "LedgerAccountCategory",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceType_StatusId",
                schema: "dbo",
                table: "PriceType",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Country",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LedgerAccount",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PriceType",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Province",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Region",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "State",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LedgerAccountCategory",
                schema: "dbo");
        }
    }
}
