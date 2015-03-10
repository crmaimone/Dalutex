namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.VW_DESENHOS_DISP_RESERVA")]
    public partial class VW_DESENHOS_DISP_RESERVA
    {
        [Key]
        [Column(Order = 0)]
        public decimal ID_ITEM_STUDIO { get; set; }

        public decimal? ID_CONTROLE_DESENV { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal ID_STUDIO { get; set; }

        [StringLength(50)]
        public string COD_STUDIO { get; set; }

        [StringLength(60)]
        public string NOME_STUDIO { get; set; }

        [StringLength(10)]
        public string DESENHO { get; set; }

        [StringLength(50)]
        public string COD_DAL { get; set; }

        [StringLength(10)]
        public string ID_CLIENTE { get; set; }
    }
}
