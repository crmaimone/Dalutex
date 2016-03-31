namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.PRE_PEDIDO_ITENS")]
    public partial class PRE_PEDIDO_ITENS
    {
        public decimal NUMERO_PRE_PEDIDO { get; set; }

        [Key]
        [Column(Order = 0)]
        public int NUMERO_PEDIDO_BLOCO { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ITEM { get; set; }

        public decimal? REDUZIDO_ITEM { get; set; }

        public decimal? STATUS_ITEM { get; set; }

        [StringLength(1)]
        public string LISO_ESTAMP { get; set; }

        [StringLength(1)]
        public string MALHA_PLANO { get; set; }

        [StringLength(1)]
        public string MODA_DECORACAO { get; set; }

        [StringLength(4)]
        public string ARTIGO { get; set; }

        [StringLength(7)]
        public string COR { get; set; }

        [StringLength(4)]
        public string DESENHO { get; set; }

        [StringLength(2)]
        public string VARIANTE { get; set; }

        public decimal? QUANTIDADE { get; set; }

        public decimal? PRECO_UNIT { get; set; }

        public decimal? VALOR_TOTAL { get; set; }

        [StringLength(2)]
        public string UM { get; set; }

        public decimal? COLECAO { get; set; }

        [StringLength(1)]
        public string PE { get; set; }

        public decimal? SIT_ITEM { get; set; }

        public DateTime? DATA_ENTREGA { get; set; }

        public DateTime? DATA_ENTREGA_DIGI { get; set; }

        [StringLength(20)]
        public string ORIGEM { get; set; }

        [StringLength(1)]
        public string COMPOSE { get; set; }

        public decimal? COD_COMPOSE { get; set; }

        public decimal? ID_TAB_PRECO { get; set; }

        public decimal? QUALIDADE { get; set; }

        public decimal? PRECODIGITADOMOEDA { get; set; }

        public decimal? PRECOLISTA { get; set; }

        public decimal? QTDEPC { get; set; }

        public string TROCA_TECNOLOGIA { get; set; }

        [NotMapped]
        public bool Novo { get; set; }
    }
}
