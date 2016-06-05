namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.VW_RASCUNHO_PEDIDOS")]
    public partial class VW_RASCUNHO_PEDIDOS
    {
        [Key]
        public decimal PEDIDO { get; set; }

        [StringLength(60)]
        public string CLIENTE { get; set; }

        [StringLength(30)]
        public string REPRESENTANTE { get; set; }

        public DateTime? DATA_EMISSAO { get; set; }

        public decimal? STATUS_PEDIDO { get; set; }
    }
}
