namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.TABELAPRECOITEM")]
    public partial class TABELAPRECOITEM
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDTABELA { get; set; }

        public long? REDUZIDO { get; set; }

        public int? COLECAO { get; set; }

        [StringLength(6)]
        public string TAMANHO { get; set; }

        public int? CODCLIENTE { get; set; }

        [StringLength(20)]
        public string QUALIDADE { get; set; }

        [StringLength(20)]
        public string QUALIDADECOMERCIAL { get; set; }

        public decimal? COD_COND_PAGTO { get; set; }

        public decimal? VALOR { get; set; }

        public decimal? PERCENTUAL { get; set; }

        public DateTime? DTINATIVACAO { get; set; }

        public byte? COMISSAO { get; set; }

        [StringLength(15)]
        public string ARTIGO { get; set; }

        public DateTime? DT_ALT { get; set; }

        [StringLength(1)]
        public string EST_LISO { get; set; }

        [StringLength(4)]
        public string DESENHO { get; set; }
    }
}
