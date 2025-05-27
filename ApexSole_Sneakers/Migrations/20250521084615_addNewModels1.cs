using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApexSole_Sneakers.Migrations
{
    /// <inheritdoc />
    public partial class addNewModels1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "OrderHeads",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "OrderHeads");
        }
    }
}
