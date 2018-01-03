namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.VW_TOTAL_VEND_DESENHO")]
    public partial class VW_TOTAL_VEND_DESENHO
    {
        [Key]
        [Column(Order = 0)]
        public string DESENHO { get; set; }

        public decimal QTDE { get; set; }
    }
}
