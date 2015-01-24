namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.CONTROLE_DESENV_ITEM_STUDIO")]
    public partial class CONTROLE_DESENV_ITEM_STUDIO
    {
        [Key]
        public decimal ID_ITEM_STUDIO { get; set; }

        public decimal ID_STUDIO { get; set; }

        [StringLength(50)]
        public string COD_STUDIO { get; set; }

        [StringLength(20)]
        public string COD_DAL { get; set; }

        public decimal? ID_DESENHISTA { get; set; }

        public decimal? VALOR { get; set; }

        public decimal? MOEDA { get; set; }

        public DateTime? VALIDADE { get; set; }

        public decimal? STATUS { get; set; }

        [StringLength(4)]
        public string DESENHO { get; set; }

        [StringLength(20)]
        public string OLD_DAL { get; set; }

        public bool? STATUS_PAGTO { get; set; }

        public DateTime? DATA_DEVOLVIDO { get; set; }

        public DateTime? DT_CADASTRO { get; set; }

        [StringLength(100)]
        public string CLIENTE_COMPROU { get; set; }

        [StringLength(100)]
        public string OBSERVACAO { get; set; }

        [StringLength(1)]
        public string IMG_PGTO { get; set; }
    }
}
