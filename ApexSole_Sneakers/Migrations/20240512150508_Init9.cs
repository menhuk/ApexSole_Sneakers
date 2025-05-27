using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApexSole_Sneakers.Migrations
{
    /// <inheritdoc />
    public partial class Init9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Jeans",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Jackets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jeans_AppUserId",
                table: "Jeans",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Jackets_AppUserId",
                table: "Jackets",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jackets_AspNetUsers_AppUserId",
                table: "Jackets",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jeans_AspNetUsers_AppUserId",
                table: "Jeans",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jackets_AspNetUsers_AppUserId",
                table: "Jackets");

            migrationBuilder.DropForeignKey(
                name: "FK_Jeans_AspNetUsers_AppUserId",
                table: "Jeans");

            migrationBuilder.DropIndex(
                name: "IX_Jeans_AppUserId",
                table: "Jeans");

            migrationBuilder.DropIndex(
                name: "IX_Jackets_AppUserId",
                table: "Jackets");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Jeans");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Jackets");
        }
    }
}
