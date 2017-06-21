namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.VW_DES_TEM_ATEND_ABERTO")]
    public partial class VW_DES_TEM_ATEND_ABERTO
    {
        [Key]
        public decimal ID_CONTROLE_DESENV { get; set; }

        [StringLength(4)]
        public string DESENHO { get; set; }
    }
}
