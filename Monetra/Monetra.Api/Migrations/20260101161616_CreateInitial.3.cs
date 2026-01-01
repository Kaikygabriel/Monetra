using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Monetra.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecurringTransaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    CostName = table.Column<string>(type: "NVARCHAR(120)", maxLength: 120, nullable: false),
                    Value = table.Column<decimal>(type: "MONEY", nullable: false),
                    MonthDayPayment = table.Column<byte>(type: "TINYINT", nullable: false),
                    PortfolioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecurringTransaction_Portfolio_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecurringTransaction_MonthDayPayment",
                table: "RecurringTransaction",
                column: "MonthDayPayment");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringTransaction_PortfolioId",
                table: "RecurringTransaction",
                column: "PortfolioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecurringTransaction");
        }
    }
}
