using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Monetra.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitial : Migration
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "Portfolios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Visible = table.Column<bool>(type: "BIT", nullable: false),
                    Title = table.Column<string>(type: "NVARCHAR(120)", maxLength: 120, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    FixedIncome_Value = table.Column<decimal>(type: "MONEY", nullable: false),
                    VariableIncome_Value = table.Column<decimal>(type: "MONEY", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_Portofolio_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Portfolios_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UserId",
                table: "Customer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_CustomerId",
                table: "Portfolios",
                column: "CustomerId");

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
                name: "Portfolios");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
