namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.PRE_PEDIDO_COND_PAG")]
    public partial class PRE_PEDIDO_COND_PAG
    {
        [Key]
        public byte ID_SGT { get; set; }

        public byte? ID_ATIVA { get; set; }
    }
}
