using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    /// <inheritdoc />
    public partial class fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientActivities",
                columns: table => new
                {
                    clientActivityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientID = table.Column<int>(type: "int", nullable: false),
                    Activity_Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activity_Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activity_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientActivities", x => x.clientActivityID);
                    table.ForeignKey(
                        name: "FK_ClientActivities_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientActivities_ClientID",
                table: "ClientActivities",
                column: "ClientID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientActivities");
        }
    }
}
