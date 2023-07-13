using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_2022.Models
{
    [Table("TB_LANCHES")]
    public class Lanche
    {
        [Key]
        [Column("LANCHE_ID")]
        public int LancheId { get; set; }

        [Required(ErrorMessage = "Informe o nome do lanche!")]
        [Display(Name = "Nome do Lanche:")]
        [StringLength(80, MinimumLength = 10, ErrorMessage = "O nome do lanche deve ter no mínimo 10 e no máximo 80 caracteres")]
        [Column("LANCHE_NOME")]
        public string LancheNome { get; set; }

        [Required(ErrorMessage = "Informe uma breve descrição!")]
        [Display(Name = "Breve descrição:")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "A descrição deve conter no mínimo 10 e no máximo 200 caracteres")]
        [Column("LANCHE_DESCRICAO_CURTA")]
        public string LancheDescricaoCurta { get; set; }

        [Required(ErrorMessage = "Informe uma descrição mais detalhada!")]
        [Display(Name = "Descrição Detalhada:")]
        [StringLength(200, MinimumLength = 20, ErrorMessage = "A descrição deve conter no mínimo 20 e no máximo 200 caracteres")]
        [Column("LANCHE_DESCRICAO_DETALHADA")]
        public string  LancheDescricaoDetalhada { get; set; }

        [Required(ErrorMessage = "Informe o preço do lanche")]
        [Display(Name = "Preço:")]
        [Column("LANCHE_VALOR",TypeName = "decimal(10,2)")]
        [Range(1, 999.99, ErrorMessage = "O preço deve estar entre 1,00 e 999,99")]
        public decimal LanchePreco { get; set; }

        [Display(Name = "Caminho Imagem:")]
        [StringLength(200, ErrorMessage = "O caminho deve ter no máximo 200 caracteres")]
        [Column("LANCHE_IMAGE")]
        public string LancheImagemURL { get; set; }

        [Display(Name = "Caminho Imagem Thumb:")]
        [StringLength(200, ErrorMessage = "O caminho deve ter no máximo 200 caracteres")]
        [Column("LANCHE_IMAGE_THUMB")]
        public string LancheImagemThumbnailURL{ get; set; }

        [Display(Name = "Preferido?")]
        [Column("LANCHE_PREFERIDO")]
        public bool LancheIsPreferido { get; set; }

        [Display(Name = "Em Estoque?")]
        [Column("LANCHE_EM_ESTOQUE")]
        public bool LancheEmEstoque { get; set; }

        //Definindo relacionamento para que crie uma FK na tabela de lanche de CategoriaId
        [Column("LANCHE_CATEGORIA_ID")]
        [Display(Name ="Categoria")]
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }    
    }
}
  