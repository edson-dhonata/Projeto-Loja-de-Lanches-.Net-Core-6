using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_2022.Migrations
{
    public partial class tabela_pedido_pedido_detalhe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_PEDIDO",
                columns: table => new
                {
                    PEDIDO_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PEDIDO_NOME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PEDIDO_SOBRENOME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PEDIDO_ENDERECO = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PEDIDO_COMPLEMENTO = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PEDIDO_CEP = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PEDIDO_ESTADO = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PEDIDO_CIDADE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PEDIDO_TELEFONE = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    PEDIDO_EMAIL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PEDIDO_VALOR_TOTAL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PEDIDO_QTD_ITENS = table.Column<int>(type: "int", nullable: false),
                    PEDIDO_DATA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PEDIDO_DATA_ENVIO = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PEDIDO", x => x.PEDIDO_ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_PEDIDO_DETALHE",
                columns: table => new
                {
                    PEDIDO_DETALHE_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PEDIDO_DETALHE_PEDIDO_ID = table.Column<int>(type: "int", nullable: false),
                    PEDIDO_DETALHE_LANCHE_ID = table.Column<int>(type: "int", nullable: false),
                    PEDIDO_DETALHE_QTD = table.Column<int>(type: "int", nullable: false),
                    PEDIDO_DETALHE_VALOR = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PEDIDO_DETALHE", x => x.PEDIDO_DETALHE_ID);
                    table.ForeignKey(
                        name: "FK_TB_PEDIDO_DETALHE_TB_LANCHES_PEDIDO_DETALHE_LANCHE_ID",
                        column: x => x.PEDIDO_DETALHE_LANCHE_ID,
                        principalTable: "TB_LANCHES",
                        principalColumn: "LANCHE_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_PEDIDO_DETALHE_TB_PEDIDO_PEDIDO_DETALHE_PEDIDO_ID",
                        column: x => x.PEDIDO_DETALHE_PEDIDO_ID,
                        principalTable: "TB_PEDIDO",
                        principalColumn: "PEDIDO_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_PEDIDO_DETALHE_PEDIDO_DETALHE_LANCHE_ID",
                table: "TB_PEDIDO_DETALHE",
                column: "PEDIDO_DETALHE_LANCHE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PEDIDO_DETALHE_PEDIDO_DETALHE_PEDIDO_ID",
                table: "TB_PEDIDO_DETALHE",
                column: "PEDIDO_DETALHE_PEDIDO_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_PEDIDO_DETALHE");

            migrationBuilder.DropTable(
                name: "TB_PEDIDO");
        }
    }
}
