namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.VW_ACORDO_DISPO_CLIENTE")]
    public partial class VW_ACORDO_DISPO_CLIENTE
    {
        [Key]
        [Column(Order = 0)]
        public decimal ID_ITEM_ACORDO_COM { get; set; }

        public int CLIENTE { get; set; }

        [StringLength(10)]
        public string ARTIGO { get; set; }

        [StringLength(2)]
        public string TECNOLOGIA { get; set; }

        public decimal QTDE_DISPONIVEL { get; set; }

        public decimal? PRECO_UNITARIO { get; set; }

        public decimal? QTDE_DISPONIVEL_TMP { get; set; }

    }
}
