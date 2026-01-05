using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Monetra.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitial8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecurringTransaction_Portfolio_PortfolioId",
                table: "RecurringTransaction");

            migrationBuilder.DropIndex(
                name: "IX_RecurringTransaction_PortfolioId",
                table: "RecurringTransaction");

            migrationBuilder.RenameColumn(
                name: "PortfolioId",
                table: "RecurringTransaction",
                newName: "ExpenseId");

            migrationBuilder.RenameColumn(
                name: "VariableIncome_Value",
                table: "Portfolio",
                newName: "VariableIncome");

            migrationBuilder.RenameColumn(
                name: "FixedIncome_Value",
                table: "Portfolio",
                newName: "Reservation");

            migrationBuilder.AddColumn<Guid>(
                name: "ExpenseId",
                table: "Portfolio",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FixedIncome",
                table: "Portfolio",
                type: "MONEY",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "Customer",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Expense",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    PortfolioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expense_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecurringTransaction_ExpenseId",
                table: "RecurringTransaction",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Portfolio_ExpenseId",
                table: "Portfolio",
                column: "ExpenseId",
                unique: true,
                filter: "[ExpenseId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_CustomerId",
                table: "Expense",
                column: "CustomerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolio_Expense_ExpenseId",
                table: "Portfolio",
                column: "ExpenseId",
                principalTable: "Expense",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringTransaction_Expense_ExpenseId",
                table: "RecurringTransaction",
                column: "ExpenseId",
                principalTable: "Expense",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portfolio_Expense_ExpenseId",
                table: "Portfolio");

            migrationBuilder.DropForeignKey(
                name: "FK_RecurringTransaction_Expense_ExpenseId",
                table: "RecurringTransaction");

            migrationBuilder.DropTable(
                name: "Expense");

            migrationBuilder.DropIndex(
                name: "IX_RecurringTransaction_ExpenseId",
                table: "RecurringTransaction");

            migrationBuilder.DropIndex(
                name: "IX_Portfolio_ExpenseId",
                table: "Portfolio");

            migrationBuilder.DropColumn(
                name: "ExpenseId",
                table: "Portfolio");

            migrationBuilder.DropColumn(
                name: "FixedIncome",
                table: "Portfolio");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "ExpenseId",
                table: "RecurringTransaction",
                newName: "PortfolioId");

            migrationBuilder.RenameColumn(
                name: "VariableIncome",
                table: "Portfolio",
                newName: "VariableIncome_Value");

            migrationBuilder.RenameColumn(
                name: "Reservation",
                table: "Portfolio",
                newName: "FixedIncome_Value");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringTransaction_PortfolioId",
                table: "RecurringTransaction",
                column: "PortfolioId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringTransaction_Portfolio_PortfolioId",
                table: "RecurringTransaction",
                column: "PortfolioId",
                principalTable: "Portfolio",
                principalColumn: "Id");
        }
    }
}
