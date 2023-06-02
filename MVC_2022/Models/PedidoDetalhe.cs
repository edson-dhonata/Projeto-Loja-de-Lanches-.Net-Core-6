using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_2022.Models
{
    [Table("TB_PEDIDO_DETALHE")]
    public class PedidoDetalhe
    {
        [Column("PEDIDO_DETALHE_ID")]
        public int PedidoDetalheId { get; set; }
        
        [Column("PEDIDO_DETALHE_PEDIDO_ID")]
        public int PedidoId { get; set; }
        
        [Column("PEDIDO_DETALHE_LANCHE_ID")]
        public int LancheId { get; set; }

        [Column("PEDIDO_DETALHE_QTD")]
        public int Quantidade { get; set; }

        [Column("PEDIDO_DETALHE_VALOR", TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; }

        public virtual Lanche Lanche { get; set; }
        public virtual Pedido Pedido { get; set; }
    }
}
