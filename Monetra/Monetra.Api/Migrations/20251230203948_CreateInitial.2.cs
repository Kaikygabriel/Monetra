using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Monetra.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mark",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "NVARCHAR(120)", maxLength: 120, nullable: false),
                    TargetAmount = table.Column<decimal>(type: "MONEY", precision: 10, scale: 2, nullable: false),
                    Deadline = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mark", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mark_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mark_CustomerId",
                table: "Mark",
                column: "CustomerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mark");
        }
    }
}
