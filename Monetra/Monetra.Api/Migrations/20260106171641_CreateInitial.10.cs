using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Monetra.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitial10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_Customer_CustomerId",
                table: "Expense");

            migrationBuilder.DropForeignKey(
                name: "FK_Mark_Customer_CustomerId",
                table: "Mark");

            migrationBuilder.DropForeignKey(
                name: "FK_Portfolio_Customer_CustomerId",
                table: "Portfolio");

            migrationBuilder.AddForeignKey(
                name: "Fk_Expense_Customer",
                table: "Expense",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Fk_Mark_Customer",
                table: "Mark",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Fk_Portfolio_Customer",
                table: "Portfolio",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Fk_Expense_Customer",
                table: "Expense");

            migrationBuilder.DropForeignKey(
                name: "Fk_Mark_Customer",
                table: "Mark");

            migrationBuilder.DropForeignKey(
                name: "Fk_Portfolio_Customer",
                table: "Portfolio");

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_Customer_CustomerId",
                table: "Expense",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mark_Customer_CustomerId",
                table: "Mark",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolio_Customer_CustomerId",
                table: "Portfolio",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
