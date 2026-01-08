using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Monetra.Api.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email_Address = table.Column<string>(type: "VARCHAR(170)", maxLength: 170, nullable: false),
                    Password = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_User_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinancialHealth = table.Column<byte>(type: "TINYINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_customer_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expense",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "MONEY", nullable: false, defaultValue: 0m),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    PortfolioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense", x => x.Id);
                    table.ForeignKey(
                        name: "Fk_Expense_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mark",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "NVARCHAR(120)", maxLength: 120, nullable: false),
                    Percentage = table.Column<int>(type: "int", nullable: false),
                    TargetAmount = table.Column<decimal>(type: "MONEY", precision: 10, scale: 2, nullable: false),
                    Deadline = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mark", x => x.Id);
                    table.ForeignKey(
                        name: "Fk_Mark_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Portfolio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Visible = table.Column<bool>(type: "BIT", nullable: false),
                    Title = table.Column<string>(type: "NVARCHAR(120)", maxLength: 120, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Reservation = table.Column<decimal>(type: "MONEY", nullable: false),
                    FixedIncome = table.Column<decimal>(type: "MONEY", nullable: false),
                    VariableIncome = table.Column<decimal>(type: "MONEY", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpenseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_Portofolio_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Portfolio_Expense_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expense",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Fk_Portfolio_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecurringTransaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    CostName = table.Column<string>(type: "NVARCHAR(120)", maxLength: 120, nullable: false),
                    Value = table.Column<decimal>(type: "MONEY", nullable: false),
                    MonthDayPayment = table.Column<byte>(type: "TINYINT", nullable: false),
                    ExpenseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecurringTransaction_Expense_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expense",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PortfolioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "MONEY", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Category = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Portfolio_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UserId",
                table: "Customer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_CustomerId",
                table: "Expense",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mark_CustomerId",
                table: "Mark",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Portfolio_CustomerId",
                table: "Portfolio",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Portfolio_ExpenseId",
                table: "Portfolio",
                column: "ExpenseId",
                unique: true,
                filter: "[ExpenseId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringTransaction_ExpenseId",
                table: "RecurringTransaction",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringTransaction_MonthDayPayment",
                table: "RecurringTransaction",
                column: "MonthDayPayment");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_PortfolioId",
                table: "Transaction",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "UX_User_Email",
                table: "User",
                column: "Email_Address",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mark");

            migrationBuilder.DropTable(
                name: "RecurringTransaction");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Portfolio");

            migrationBuilder.DropTable(
                name: "Expense");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
