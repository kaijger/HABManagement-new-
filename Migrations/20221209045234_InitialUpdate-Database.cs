using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HABManagement.Migrations
{
    public partial class InitialUpdateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category2",
                table: "Kakei",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category2",
                table: "Kakei");
        }
    }
}
