using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Monetra.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitial6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Portfolio_PortfolioId1",
                table: "Transaction");

            migrationBuilder.RenameColumn(
                name: "PortfolioId1",
                table: "Transaction",
                newName: "PortfolioId");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_PortfolioId1",
                table: "Transaction",
                newName: "IX_Transaction_PortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Portfolio_PortfolioId",
                table: "Transaction",
                column: "PortfolioId",
                principalTable: "Portfolio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Portfolio_PortfolioId",
                table: "Transaction");

            migrationBuilder.RenameColumn(
                name: "PortfolioId",
                table: "Transaction",
                newName: "PortfolioId1");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_PortfolioId",
                table: "Transaction",
                newName: "IX_Transaction_PortfolioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Portfolio_PortfolioId1",
                table: "Transaction",
                column: "PortfolioId1",
                principalTable: "Portfolio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
