namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.GRUPO_CLIENTE_SGT")]
    public partial class GRUPO_CLIENTE_SGT
    {
        public decimal? ID_GRUPO { get; set; }

        public decimal? ID_CLIENTE_SGT { get; set; }

        [Key]
        public decimal PK_GRUPO_CLIENTE_SGT { get; set; }
    }
}
