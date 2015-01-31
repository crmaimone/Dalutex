namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.PRE_PEDIDO_ATEND")]
    public partial class PRE_PEDIDO_ATEND
    {
        [Key]
        public decimal COD_ATEND { get; set; }

        [StringLength(100)]
        public string DESCRI_ATEND { get; set; }
    }
}
