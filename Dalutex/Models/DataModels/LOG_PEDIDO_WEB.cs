namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.LOG_PEDIDO_WEB")]
    public partial class LOG_PEDIDO_WEB
    {
        [Key]
        public decimal ID_LOG_PEDIDO_WEB { get; set; }

        [StringLength(256)]
        public string DS_LOG_PEDIDO_WEB { get; set; }

        public DateTime? MM_LOG_PEDIDO_WEB { get; set; }

        [StringLength(100)]
        public string USU_LOG_PEDIDO_WEB { get; set; }

        [StringLength(12)]
        public string TP_LOG_PEDIDO_WEB { get; set; }
    }
}
