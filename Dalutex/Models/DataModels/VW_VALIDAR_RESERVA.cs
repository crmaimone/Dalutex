namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.VW_VALIDAR_RESERVA")]
    public partial class VW_VALIDAR_RESERVA
    {
        public decimal? ID_REP { get; set; }

        [StringLength(30)]
        public string REPRESENTANTE { get; set; }

        [StringLength(60)]
        public string CLIENTE { get; set; }

        [Key]
        public decimal PEDIDO { get; set; }

        public DateTime? DATA_EMISSAO { get; set; }

        public decimal? ITEM_PEDIDO { get; set; }

        public decimal? ID_CONTROLE { get; set; }

        [StringLength(50)]
        public string COD_STUDIO { get; set; }

        [StringLength(60)]
        public string STUDIO { get; set; }

        [StringLength(10)]
        public string DESENHO { get; set; }

        [StringLength(50)]
        public string COD_DAL { get; set; }

        [StringLength(4)]
        public string VARIANTE { get; set; }

        [StringLength(30)]
        public string DIGITADOR { get; set; }
    }
}
