using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApexSole_Sneakers.Migrations
{
    /// <inheritdoc />
    public partial class AddPreview3D2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "48be39b8-55a5-41e1-9b9d-07728d041a56");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c467d207-e001-4d53-b6e8-d11022dee930");

            migrationBuilder.AlterColumn<string>(
                name: "Preview3d",
                table: "Sneakers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "21b55d8c-bd31-4ce4-a0fc-e406083e5360", null, "Admin", "ADMIN" },
                    { "be5d4bc4-e7d3-418b-a34d-64730d033fd2", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b55d8c-bd31-4ce4-a0fc-e406083e5360");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be5d4bc4-e7d3-418b-a34d-64730d033fd2");

            migrationBuilder.AlterColumn<string>(
                name: "Preview3d",
                table: "Sneakers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "48be39b8-55a5-41e1-9b9d-07728d041a56", null, "Admin", "ADMIN" },
                    { "c467d207-e001-4d53-b6e8-d11022dee930", null, "User", "USER" }
                });
        }
    }
}
