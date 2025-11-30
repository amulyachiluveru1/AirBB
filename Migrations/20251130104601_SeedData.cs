using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AirBB.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    DOB = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    SSN = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    UserType = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Residences",
                columns: table => new
                {
                    ResidenceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LocationId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Accommodation = table.Column<int>(type: "INTEGER", nullable: false),
                    Bedrooms = table.Column<int>(type: "INTEGER", nullable: false),
                    Bathrooms = table.Column<decimal>(type: "TEXT", nullable: false),
                    BuiltYear = table.Column<int>(type: "INTEGER", nullable: false),
                    ImageFileName = table.Column<string>(type: "TEXT", nullable: true),
                    GuestNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    PricePerNight = table.Column<decimal>(type: "TEXT", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Residences_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReservationStartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReservationEndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ResidenceId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClientUserId = table.Column<int>(type: "INTEGER", nullable: false)
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
                table: "Clients",
                columns: new[] { "ClientId", "DOB", "Email", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "john@example.com", "John Doe", "1234567890" },
                    { 2, new DateTime(1988, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "emma@example.com", "Emma Watson", "9876543210" },
                    { 3, new DateTime(1985, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "chris@example.com", "Chris Evans", "7778889999" },
                    { 4, new DateTime(1993, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "mia@example.com", "Mia Smith", "5551237890" },
                    { 5, new DateTime(1995, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "liam@example.com", "Liam Brown", "4442221111" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationId", "Name" },
                values: new object[,]
                {
                    { 1, "New York" },
                    { 2, "Los Angeles" },
                    { 3, "Chicago" },
                    { 4, "Boston" },
                    { 5, "Miami" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Name", "PhoneNumber", "SSN", "UserType" },
                values: new object[,]
                {
                    { 1, "owner1@mail.com", "Owner One", "7001002003", "SSN001", "Owner" },
                    { 2, "owner2@mail.com", "Owner Two", "7001002004", "SSN002", "Owner" },
                    { 3, "clientA@mail.com", "Client A", "7001002005", "SSN003", "Client" },
                    { 4, "clientB@mail.com", "Client B", "7001002006", "SSN004", "Client" },
                    { 5, "clientC@mail.com", "Client C", "7001002007", "SSN005", "Client" }
                });

            migrationBuilder.InsertData(
                table: "Residences",
                columns: new[] { "ResidenceId", "Accommodation", "Bathrooms", "Bedrooms", "BuiltYear", "GuestNumber", "ImageFileName", "LocationId", "Name", "OwnerId", "PricePerNight" },
                values: new object[,]
                {
                    { 1, 4, 1.5m, 2, 2010, 5, "atl_house.jpg", 1, "Central Apartment", 1, 120m },
                    { 2, 6, 2m, 3, 2015, 3, "chi_loop.jpg", 2, "Ocean View House", 2, 200m },
                    { 3, 2, 1m, 1, 2020, 10, "nyc_studio.jpg", 3, "City Studio", 1, 90m },
                    { 4, 5, 1.5m, 2, 2018, 1, "miami_beach.jpg", 4, "Modern Condo", 2, 150m }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "ReservationId", "ClientUserId", "ReservationEndDate", "ReservationStartDate", "ResidenceId" },
                values: new object[,]
                {
                    { 1, 3, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 4, new DateTime(2025, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, 5, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ResidenceId",
                table: "Reservations",
                column: "ResidenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Residences_LocationId",
                table: "Residences",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Residences_OwnerId",
                table: "Residences",
                column: "OwnerId");
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

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
