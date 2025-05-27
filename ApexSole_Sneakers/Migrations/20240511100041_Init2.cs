using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApexSole_Sneakers.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SneakersType",
                table: "Tshirts",
                newName: "TshirtType");

            migrationBuilder.RenameColumn(
                name: "SneakersSize",
                table: "Tshirts",
                newName: "TshirtSize");

            migrationBuilder.RenameColumn(
                name: "SneakersPrice",
                table: "Tshirts",
                newName: "TshirtPrice");

            migrationBuilder.RenameColumn(
                name: "SneakersName",
                table: "Tshirts",
                newName: "TshirtName");

            migrationBuilder.RenameColumn(
                name: "SneakersColor",
                table: "Tshirts",
                newName: "TshirtColor");

            migrationBuilder.RenameColumn(
                name: "SneakersBrand",
                table: "Tshirts",
                newName: "TshirtBrand");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TshirtType",
                table: "Tshirts",
                newName: "SneakersType");

            migrationBuilder.RenameColumn(
                name: "TshirtSize",
                table: "Tshirts",
                newName: "SneakersSize");

            migrationBuilder.RenameColumn(
                name: "TshirtPrice",
                table: "Tshirts",
                newName: "SneakersPrice");

            migrationBuilder.RenameColumn(
                name: "TshirtName",
                table: "Tshirts",
                newName: "SneakersName");

            migrationBuilder.RenameColumn(
                name: "TshirtColor",
                table: "Tshirts",
                newName: "SneakersColor");

            migrationBuilder.RenameColumn(
                name: "TshirtBrand",
                table: "Tshirts",
                newName: "SneakersBrand");
        }
    }
}
