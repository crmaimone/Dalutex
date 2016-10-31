namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.VW_CUS_CONS_TAB_PRECO")]
    public partial class VW_CUS_CONS_TAB_PRECO
    {
        [Key]
        [Column(Order = 0)]
        public decimal? ID_TABELA_PRECO_ITEM { get; set; }

        public decimal? ID_USUARIO { get; set; }

        [StringLength(25)]
        public string NOME_USU { get; set; }

        public decimal? ID_TABELA_PRECO { get; set; }

        [StringLength(10)]
        public string ARTIGO { get; set; }

        [StringLength(10)]
        public string TECNOLOGIA { get; set; }

        [StringLength(10)]
        public string CLASSIFICACAO { get; set; }

        [StringLength(40)]
        public string CONDICAO_PAGAMENTO { get; set; }

        [StringLength(2)]
        public string QUALIDADE_COMERCIAL { get; set; }

        public decimal? COMISSAO { get; set; }

        public decimal? PRECO { get; set; }

        public double? RENDIMENTO { get; set; }

        public double? LARGURA { get; set; }

        public double? GRAMATURA { get; set; }

        [StringLength(1410)]
        public string COMPOSICAO { get; set; }

        public double? PRECO_M2 { get; set; }
        public double? PRECO_M { get; set; }

    }
}
