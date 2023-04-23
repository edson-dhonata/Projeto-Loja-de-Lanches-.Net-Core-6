using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_2022.Models
{
    [Table("TB_CATEGORIAS")]
    public class Categoria
    {
        [Key]
        [Column("CATEGORIA_ID")]
        public int CategoriaId { get; set; }

        [StringLength(100,ErrorMessage = "Limite máximo de caracteres é 100.")]
        [Required(ErrorMessage = "Informe o nome da categoria!")]
        [Display(Name = "Nome Categeoria:")]
        [Column("CATEGORIA_NOME")]
        public string CategoriaNome { get; set; }

        [StringLength(200, MinimumLength = 5, ErrorMessage = "Limite máximo de caracteres é 200.")]
        [Required(ErrorMessage = "Informe uma descrição da categoria!")]
        [Display(Name = "Descrição:")]
        [Column("CATEGORIA_DESCRICAO")]
        public string CategoriaDescricao { get; set; }

        public List<Lanche> Lanches { get; set; }
    }
}
