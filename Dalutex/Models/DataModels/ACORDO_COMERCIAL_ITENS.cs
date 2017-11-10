namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.ACORDO_COMERCIAL_ITENS")]
    public partial class ACORDO_COMERCIAL_ITENS
    {
        [Key]
        public decimal ID_ITEM_ACORDO_COM { get; set; }

        public decimal ID_ACORDO { get; set; }

        [StringLength(10)]
        public string ARTIGO { get; set; }

        [StringLength(10)]
        public string DESENHO { get; set; }

        [StringLength(10)]
        public string VARIANTE { get; set; }

        [StringLength(10)]
        public string COR { get; set; }

        public decimal QUANTIDADES { get; set; }

        [StringLength(2)]
        public string UM { get; set; }

        public decimal? PRECO_UNITARIO { get; set; }

        public decimal QTDE_DISPONIVEL { get; set; }

        [StringLength(2)]
        public string TECNOLOGIA { get; set; }

        public decimal QTDE_DISPONIVEL_TMP { get; set; }

        public decimal? CONDICAO_PGTO { get; set; }

        [StringLength(1)]
        public string QUALIDADE_COM { get; set; }

        public decimal? COMISSAO { get; set; }

        public decimal? ID_GRUPO_COND_PGTO { get; set; }

        public decimal? MARGEM { get; set; }

        public bool STATUS_ITEM_ACORDO { get; set; }
    }
}
