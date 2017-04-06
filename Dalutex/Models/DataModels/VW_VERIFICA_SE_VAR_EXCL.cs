namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.VW_VERIFICA_SE_VAR_EXCL")]
    public partial class VW_VERIFICA_SE_VAR_EXCL
    {
        public decimal? ID_VAR_EXC { get; set; }

        [Key]
        [StringLength(4)]
        public string DESENHO { get; set; }

        [StringLength(2)]
        public string VARIANTE { get; set; }

        public decimal? ID_GRUPO { get; set; }

        public decimal? ID_CLIENTE_SGT { get; set; }

        public string GRUPO { get; set; }
    }
}
