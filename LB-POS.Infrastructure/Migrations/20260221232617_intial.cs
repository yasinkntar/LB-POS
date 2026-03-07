using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LB_POS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class intial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SyndicateLicenseNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ActivityCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Address_AdditionalInformation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_BuildingNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address_Country = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Address_Floor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address_Governate = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address_Landmark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_PostalCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Address_RegionCity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address_Room = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Branches");
        }
    }
}
