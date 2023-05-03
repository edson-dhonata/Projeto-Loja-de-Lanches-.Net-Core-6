using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_2022.Models
{
    [Table("TB_ITENS_CARRINHO")]
    public class CarrinhoCompraItem
    {
        [Column("ITEM_CARRINHO_ID")]
        public int CarrinhoCompraItemId { get; set; }

        [Column("ITEM_LANCHE_ID")]
        public int LancheId { get; set; }
        public virtual Lanche Lanche { get; set; }

        [Column("ITEM_QUANTIDADE")]
        public int Quantidade { get; set; }

        [Column("ITEM_CARRINHO_COMPRA_ID")]
        public string CarrinhoCompraId { get; set; }
    }
}
