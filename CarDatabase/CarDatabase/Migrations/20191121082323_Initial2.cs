using Microsoft.EntityFrameworkCore.Migrations;

namespace CarDatabase.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CarMakes_Make",
                table: "CarMakes",
                column: "Make",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CarMakes_Make",
                table: "CarMakes");
        }
    }
}
