using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "MobileNumber",
                table: "Clients",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Deals",
                columns: table => new
                {
                    deal_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientID = table.Column<int>(type: "int", nullable: false),
                    deal_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deal_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deal_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total_Amount = table.Column<int>(type: "int", nullable: false),
                    amount_received = table.Column<int>(type: "int", nullable: false),
                    pending_amount = table.Column<int>(type: "int", nullable: false),
                    closing_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    product_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deals", x => x.deal_id);
                    table.ForeignKey(
                        name: "FK_Deals_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Transaction_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    deal_id = table.Column<int>(type: "int", nullable: false),
                    Transaction_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Transaction_id);
                    table.ForeignKey(
                        name: "FK_Transactions_Deals_deal_id",
                        column: x => x.deal_id,
                        principalTable: "Deals",
                        principalColumn: "deal_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deals_ClientID",
                table: "Deals",
                column: "ClientID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_deal_id",
                table: "Transactions",
                column: "deal_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Deals");

            migrationBuilder.AlterColumn<int>(
                name: "MobileNumber",
                table: "Clients",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
