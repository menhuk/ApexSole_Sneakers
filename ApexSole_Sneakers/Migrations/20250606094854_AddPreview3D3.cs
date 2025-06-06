using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApexSole_Sneakers.Migrations
{
    /// <inheritdoc />
    public partial class AddPreview3D3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b55d8c-bd31-4ce4-a0fc-e406083e5360");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be5d4bc4-e7d3-418b-a34d-64730d033fd2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a0afca0d-0994-48cd-ac0c-2ff854471489", null, "User", "USER" },
                    { "c09e9c31-60f0-4ff6-985b-b95cae12af23", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0afca0d-0994-48cd-ac0c-2ff854471489");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c09e9c31-60f0-4ff6-985b-b95cae12af23");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "21b55d8c-bd31-4ce4-a0fc-e406083e5360", null, "Admin", "ADMIN" },
                    { "be5d4bc4-e7d3-418b-a34d-64730d033fd2", null, "User", "USER" }
                });
        }
    }
}
