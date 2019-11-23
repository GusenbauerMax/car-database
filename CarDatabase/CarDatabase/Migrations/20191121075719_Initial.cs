using Microsoft.EntityFrameworkCore.Migrations;

namespace CarDatabase.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarMakes",
                columns: table => new
                {
                    CarMakeID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Make = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarMakes", x => x.CarMakeID);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonID);
                });

            migrationBuilder.CreateTable(
                name: "CarModels",
                columns: table => new
                {
                    CarModelID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Model = table.Column<string>(maxLength: 100, nullable: false),
                    CarMakeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarModels", x => x.CarModelID);
                    table.ForeignKey(
                        name: "FK_CarModels_CarMakes_CarMakeID",
                        column: x => x.CarMakeID,
                        principalTable: "CarMakes",
                        principalColumn: "CarMakeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ownerships",
                columns: table => new
                {
                    OwnershipID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VehicleIdentificationNumber = table.Column<string>(maxLength: 100, nullable: false),
                    PersonID = table.Column<int>(nullable: false),
                    CarModelID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ownerships", x => x.OwnershipID);
                    table.ForeignKey(
                        name: "FK_Ownerships_CarModels_CarModelID",
                        column: x => x.CarModelID,
                        principalTable: "CarModels",
                        principalColumn: "CarModelID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ownerships_Persons_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Persons",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarModels_CarMakeID",
                table: "CarModels",
                column: "CarMakeID");

            migrationBuilder.CreateIndex(
                name: "IX_Ownerships_PersonID",
                table: "Ownerships",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Ownerships_VehicleIdentificationNumber",
                table: "Ownerships",
                column: "VehicleIdentificationNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ownerships_CarModelID_OwnershipID",
                table: "Ownerships",
                columns: new[] { "CarModelID", "OwnershipID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ownerships");

            migrationBuilder.DropTable(
                name: "CarModels");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "CarMakes");
        }
    }
}
