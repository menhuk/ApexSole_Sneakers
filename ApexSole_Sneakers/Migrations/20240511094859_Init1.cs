using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApexSole_Sneakers.Migrations
{
    /// <inheritdoc />
    public partial class Init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jackets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JacketName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JacketBrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JacketType = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    JacketColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JacketSize = table.Column<int>(type: "int", nullable: false),
                    JacketPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jackets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jeans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JeansName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JeansBrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JeansType = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    JeansColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JeansSize = table.Column<int>(type: "int", nullable: false),
                    JeansPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jeans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tshirts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SneakersName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SneakersBrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SneakersType = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    SneakersColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SneakersSize = table.Column<int>(type: "int", nullable: false),
                    SneakersPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tshirts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Apparel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JeansId = table.Column<int>(type: "int", nullable: false),
                    TshirtId = table.Column<int>(type: "int", nullable: false),
                    JacketId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apparel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apparel_Jackets_JacketId",
                        column: x => x.JacketId,
                        principalTable: "Jackets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Apparel_Jeans_JeansId",
                        column: x => x.JeansId,
                        principalTable: "Jeans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Apparel_Tshirts_TshirtId",
                        column: x => x.TshirtId,
                        principalTable: "Tshirts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apparel_JacketId",
                table: "Apparel",
                column: "JacketId");

            migrationBuilder.CreateIndex(
                name: "IX_Apparel_JeansId",
                table: "Apparel",
                column: "JeansId");

            migrationBuilder.CreateIndex(
                name: "IX_Apparel_TshirtId",
                table: "Apparel",
                column: "TshirtId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apparel");

            migrationBuilder.DropTable(
                name: "Jackets");

            migrationBuilder.DropTable(
                name: "Jeans");

            migrationBuilder.DropTable(
                name: "Tshirts");
        }
    }
}
