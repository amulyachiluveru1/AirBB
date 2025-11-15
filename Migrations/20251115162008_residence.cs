using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AirBB.Migrations
{
    /// <inheritdoc />
    public partial class residence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResidencePicture",
                table: "Residences");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Clients",
                newName: "ClientId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Residences",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Accommodation",
                table: "Residences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Bathrooms",
                table: "Residences",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Bedrooms",
                table: "Residences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BuiltYear",
                table: "Residences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                table: "Residences",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Residences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ClientUserId",
                table: "Reservations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Locations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SSN = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "DOB", "Email", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, null, "clientA@mail.com", "Client A", "9999999999" },
                    { 2, null, "clientB@mail.com", "Client B", "8888888888" }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "ReservationId", "ClientUserId", "ReservationEndDate", "ReservationStartDate", "ResidenceId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 101 },
                    { 2, 2, new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 103 }
                });

            migrationBuilder.UpdateData(
                table: "Residences",
                keyColumn: "ResidenceId",
                keyValue: 101,
                columns: new[] { "Accommodation", "BathroomNumber", "Bathrooms", "BedroomNumber", "Bedrooms", "BuiltYear", "GuestNumber", "ImageFileName", "OwnerId" },
                values: new object[] { 3, 0, 1m, 0, 1, 2000, 0, "chi_loop.jpg", 201 });

            migrationBuilder.UpdateData(
                table: "Residences",
                keyColumn: "ResidenceId",
                keyValue: 102,
                columns: new[] { "Accommodation", "BathroomNumber", "Bathrooms", "Bedrooms", "BuiltYear", "GuestNumber", "ImageFileName", "OwnerId" },
                values: new object[] { 2, 0, 1m, 0, 2005, 0, "nyc_studio.jpg", 202 });

            migrationBuilder.UpdateData(
                table: "Residences",
                keyColumn: "ResidenceId",
                keyValue: 103,
                columns: new[] { "Accommodation", "BathroomNumber", "Bathrooms", "BedroomNumber", "Bedrooms", "BuiltYear", "GuestNumber", "ImageFileName", "OwnerId" },
                values: new object[] { 6, 0, 2m, 0, 3, 1998, 0, "miami_beach.jpg", 203 });

            migrationBuilder.UpdateData(
                table: "Residences",
                keyColumn: "ResidenceId",
                keyValue: 104,
                columns: new[] { "Accommodation", "BathroomNumber", "Bathrooms", "BedroomNumber", "Bedrooms", "BuiltYear", "GuestNumber", "ImageFileName", "Name", "OwnerId" },
                values: new object[] { 5, 0, 2m, 0, 3, 2010, 0, "atl_house.jpg", "Atlanta Suburban House", 204 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Name", "PhoneNumber", "SSN", "UserType" },
                values: new object[,]
                {
                    { 201, "john@mail.com", "John Owner", "1111111111", "111-22-3333", "Owner" },
                    { 202, "emma@mail.com", "Emma Owner", "2222222222", "444-55-6666", "Owner" },
                    { 203, "mike@mail.com", "Michael Owner", "3333333333", "777-88-9999", "Owner" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Accommodation",
                table: "Residences");

            migrationBuilder.DropColumn(
                name: "Bathrooms",
                table: "Residences");

            migrationBuilder.DropColumn(
                name: "Bedrooms",
                table: "Residences");

            migrationBuilder.DropColumn(
                name: "BuiltYear",
                table: "Residences");

            migrationBuilder.DropColumn(
                name: "ImageFileName",
                table: "Residences");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Residences");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Clients",
                newName: "UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Residences",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "ResidencePicture",
                table: "Residences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ClientUserId",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                table: "Residences",
                keyColumn: "ResidenceId",
                keyValue: 101,
                columns: new[] { "BathroomNumber", "BedroomNumber", "GuestNumber", "ResidencePicture" },
                values: new object[] { 1, 1, 3, "chi_loop.jpg" });

            migrationBuilder.UpdateData(
                table: "Residences",
                keyColumn: "ResidenceId",
                keyValue: 102,
                columns: new[] { "BathroomNumber", "GuestNumber", "ResidencePicture" },
                values: new object[] { 1, 2, "nyc_studio.jpg" });

            migrationBuilder.UpdateData(
                table: "Residences",
                keyColumn: "ResidenceId",
                keyValue: 103,
                columns: new[] { "BathroomNumber", "BedroomNumber", "GuestNumber", "ResidencePicture" },
                values: new object[] { 2, 3, 6, "miami_beach.jpg" });

            migrationBuilder.UpdateData(
                table: "Residences",
                keyColumn: "ResidenceId",
                keyValue: 104,
                columns: new[] { "BathroomNumber", "BedroomNumber", "GuestNumber", "Name", "ResidencePicture" },
                values: new object[] { 2, 3, 5, "Atlanta Suburban Single Family House", "atl_house.jpg" });
        }
    }
}
