using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApexSole_Sneakers.Migrations
{
    /// <inheritdoc />
    public partial class Init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TshirtDescription",
                table: "Tshirts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SneakersDescription",
                table: "Sneakers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JeansDescription",
                table: "Jeans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JacketDescription",
                table: "Jackets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TshirtDescription",
                table: "Tshirts");

            migrationBuilder.DropColumn(
                name: "SneakersDescription",
                table: "Sneakers");

            migrationBuilder.DropColumn(
                name: "JeansDescription",
                table: "Jeans");

            migrationBuilder.DropColumn(
                name: "JacketDescription",
                table: "Jackets");
        }
    }
}
