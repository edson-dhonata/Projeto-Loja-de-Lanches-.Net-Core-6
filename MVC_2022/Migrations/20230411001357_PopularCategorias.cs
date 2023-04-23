using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_2022.Migrations
{
    public partial class PopularCategorias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO TB_CATEGORIAS (CATEGORIA_NOME, CATEGORIA_DESCRICAO) VALUES ('Normal', 'Lanche feito com igredientes normais.') ");
            migrationBuilder.Sql("INSERT INTO TB_CATEGORIAS (CATEGORIA_NOME, CATEGORIA_DESCRICAO) VALUES ('Integral', 'Lanche feito com igredientes integral e natural.') ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM TB_CATEGORIAS");
        }
    }
}
