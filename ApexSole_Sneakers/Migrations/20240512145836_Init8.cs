using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApexSole_Sneakers.Migrations
{
    /// <inheritdoc />
    public partial class Init8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Tshirts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tshirts_AppUserId",
                table: "Tshirts",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tshirts_AspNetUsers_AppUserId",
                table: "Tshirts",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tshirts_AspNetUsers_AppUserId",
                table: "Tshirts");

            migrationBuilder.DropIndex(
                name: "IX_Tshirts_AppUserId",
                table: "Tshirts");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Tshirts");
        }
    }
}
