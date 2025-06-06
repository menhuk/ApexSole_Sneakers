using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApexSole_Sneakers.Migrations
{
    /// <inheritdoc />
    public partial class AddPreview3D : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c4828d1-66ae-4711-bf41-c85728edcfe9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b3222aa-505a-4b2a-a7ff-a3b20c883dd7");

            migrationBuilder.AddColumn<string>(
                name: "Preview3d",
                table: "Sneakers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "48be39b8-55a5-41e1-9b9d-07728d041a56", null, "Admin", "ADMIN" },
                    { "c467d207-e001-4d53-b6e8-d11022dee930", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "48be39b8-55a5-41e1-9b9d-07728d041a56");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c467d207-e001-4d53-b6e8-d11022dee930");

            migrationBuilder.DropColumn(
                name: "Preview3d",
                table: "Sneakers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4c4828d1-66ae-4711-bf41-c85728edcfe9", null, "Admin", "ADMIN" },
                    { "8b3222aa-505a-4b2a-a7ff-a3b20c883dd7", null, "User", "USER" }
                });
        }
    }
}
