using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_2022.Migrations
{
    public partial class CarrinhoCompraItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_ITENS_CARRINHO",
                columns: table => new
                {
                    ITEM_CARRINHO_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ITEM_LANCHE_ID = table.Column<int>(type: "int", nullable: false),
                    ITEM_QUANTIDADE = table.Column<int>(type: "int", nullable: false),
                    ITEM_CARRINHO_COMPRA_ID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ITENS_CARRINHO", x => x.ITEM_CARRINHO_ID);
                    table.ForeignKey(
                        name: "FK_TB_ITENS_CARRINHO_TB_LANCHES_ITEM_LANCHE_ID",
                        column: x => x.ITEM_LANCHE_ID,
                        principalTable: "TB_LANCHES",
                        principalColumn: "LANCHE_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_ITENS_CARRINHO_ITEM_LANCHE_ID",
                table: "TB_ITENS_CARRINHO",
                column: "ITEM_LANCHE_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_ITENS_CARRINHO");
        }
    }
}
