using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AirBB.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "Residences",
                columns: table => new
                {
                    ResidenceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResidencePicture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    GuestNumber = table.Column<int>(type: "int", nullable: false),
                    BedroomNumber = table.Column<int>(type: "int", nullable: false),
                    BathroomNumber = table.Column<int>(type: "int", nullable: false),
                    PricePerNight = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residences", x => x.ResidenceId);
                    table.ForeignKey(
                        name: "FK_Residences_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReservationEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResidenceId = table.Column<int>(type: "int", nullable: false),
                    ClientUserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservations_Residences_ResidenceId",
                        column: x => x.ResidenceId,
                        principalTable: "Residences",
                        principalColumn: "ResidenceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationId", "Name" },
                values: new object[,]
                {
                    { 1, "Chicago" },
                    { 2, "New York" },
                    { 3, "Miami" },
                    { 4, "Atlanta" }
                });

            migrationBuilder.InsertData(
                table: "Residences",
                columns: new[] { "ResidenceId", "BathroomNumber", "BedroomNumber", "GuestNumber", "LocationId", "Name", "PricePerNight", "ResidencePicture" },
                values: new object[,]
                {
                    { 101, 1, 1, 3, 1, "Chicago Loop Apartment", 120m, "chi_loop.jpg" },
                    { 102, 1, 0, 2, 2, "NYC Cozy Studio", 150m, "nyc_studio.jpg" },
                    { 103, 2, 3, 6, 3, "Miami Beach House", 320m, "miami_beach.jpg" },
                    { 104, 2, 3, 5, 4, "Atlanta Suburban Single Family House", 180m, "atl_house.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ResidenceId",
                table: "Reservations",
                column: "ResidenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Residences_LocationId",
                table: "Residences",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Residences");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
