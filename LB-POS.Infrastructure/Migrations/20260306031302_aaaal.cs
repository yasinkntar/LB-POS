using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LB_POS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class aaaal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_PostalCodeEn",
                table: "Branches");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address_PostalCodeEn",
                table: "Branches",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);
        }
    }
}
