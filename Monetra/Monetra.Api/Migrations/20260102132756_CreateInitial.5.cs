using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Monetra.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitial5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecurringTransaction_Portfolio_PortfolioId",
                table: "RecurringTransaction");

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringTransaction_Portfolio_PortfolioId",
                table: "RecurringTransaction",
                column: "PortfolioId",
                principalTable: "Portfolio",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecurringTransaction_Portfolio_PortfolioId",
                table: "RecurringTransaction");

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringTransaction_Portfolio_PortfolioId",
                table: "RecurringTransaction",
                column: "PortfolioId",
                principalTable: "Portfolio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
