using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_2022.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_CATEGORIAS",
                columns: table => new
                {
                    CATEGORIA_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CATEGORIA_NOME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CATEGORIA_DESCRICAO = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CATEGORIAS", x => x.CATEGORIA_ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_LANCHES",
                columns: table => new
                {
                    LANCHE_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LANCHE_NOME = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    LANCHE_DESCRICAO_CURTA = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LANCHE_DESCRICAO_DETALHADA = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LANCHE_VALOR = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    LANCHE_IMAGE = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LANCHE_IMAGE_THUMB = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LANCHE_PREFERIDO = table.Column<bool>(type: "bit", nullable: false),
                    LANCHE_EM_ESTOQUE = table.Column<bool>(type: "bit", nullable: false),
                    LANCHE_CATEGORIA_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_LANCHES", x => x.LANCHE_ID);
                    table.ForeignKey(
                        name: "FK_TB_LANCHES_TB_CATEGORIAS_LANCHE_CATEGORIA_ID",
                        column: x => x.LANCHE_CATEGORIA_ID,
                        principalTable: "TB_CATEGORIAS",
                        principalColumn: "CATEGORIA_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_LANCHES_LANCHE_CATEGORIA_ID",
                table: "TB_LANCHES",
                column: "LANCHE_CATEGORIA_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_LANCHES");

            migrationBuilder.DropTable(
                name: "TB_CATEGORIAS");
        }
    }
}
