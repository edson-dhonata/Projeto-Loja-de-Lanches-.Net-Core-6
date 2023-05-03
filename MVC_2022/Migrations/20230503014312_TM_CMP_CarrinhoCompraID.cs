using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_2022.Migrations
{
    public partial class TM_CMP_CarrinhoCompraID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ITEM_CARRINHO_COMPRA_ID",
                table: "TB_ITENS_CARRINHO",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ITEM_CARRINHO_COMPRA_ID",
                table: "TB_ITENS_CARRINHO",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);
        }
    }
}
