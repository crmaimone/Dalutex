namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.VW_PEDIDO_ROM_FAT")]
    public partial class VW_PEDIDO_ROM_FAT
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PEDIDO { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short ITEMPEDIDO { get; set; }

        public int? REDUZIDOITEM { get; set; }

        [Column(TypeName = "float")]
        public decimal? QTDE_PEDIDA { get; set; }

        public DateTime? EXPEDIREM { get; set; }

        public decimal? QTDE_ABERTA { get; set; }

        [StringLength(2)]
        public string UM { get; set; }

        [StringLength(7)]
        public string COD_CLI { get; set; }

        [StringLength(6)]
        public string MES { get; set; }

        public int? EMITIDOEM { get; set; }

        public int? IDREPRESENTANTE { get; set; }

        public int? EXPEDIREM_INT { get; set; }

        public int? QUALIDADE_COMERCIAL { get; set; }

        [Column(TypeName = "float")]
        public decimal? PRECOUNITARIO { get; set; }

        [StringLength(1)]
        public string ESTOQUE { get; set; }

        public decimal? QTDE_FAT { get; set; }

        public decimal? QTDE_ROM { get; set; }

        public short? CANAL_VENDAS { get; set; }

        public short? TIPOPEDIDO { get; set; }

        public int? PERIODO_PLANO { get; set; }

        public short? COD_COND_PGTO { get; set; }

        [StringLength(250)]
        public string OBSERV_PED { get; set; }
    }
}
