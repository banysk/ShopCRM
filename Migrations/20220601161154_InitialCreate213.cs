using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopCRM.Migrations
{
    public partial class InitialCreate213 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemporaryCustomers");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Orders");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerGuid",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerGuid",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TemporaryCustomers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Building = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Flat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Index = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporaryCustomers", x => x.Id);
                });
        }
    }
}
