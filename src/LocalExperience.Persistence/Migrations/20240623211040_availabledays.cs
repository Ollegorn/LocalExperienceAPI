using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ollegorn.LocalExperience.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class availabledays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Categories_CategoryId",
                table: "Activities");

            migrationBuilder.AddColumn<int>(
                name: "AvailableDays",
                table: "Activities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReservedFor = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CustomersName = table.Column<string>(type: "text", nullable: false),
                    NumberOfPeople = table.Column<int>(type: "integer", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    CustomersNumber = table.Column<string>(type: "text", nullable: false),
                    CustomersEmail = table.Column<string>(type: "text", nullable: false),
                    Coupon = table.Column<string>(type: "text", nullable: false),
                    ActivityId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Booking_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ActivityId",
                table: "Booking",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Categories_CategoryId",
                table: "Activities",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Categories_CategoryId",
                table: "Activities");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropColumn(
                name: "AvailableDays",
                table: "Activities");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Categories_CategoryId",
                table: "Activities",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
