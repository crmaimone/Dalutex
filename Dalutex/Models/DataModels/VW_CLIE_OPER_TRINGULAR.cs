namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.VW_CLIE_OPER_TRINGULAR")]
    public partial class VW_CLIE_OPER_TRINGULAR
    {
        [Key]
        public decimal? ID_ { get; set; }
        
        [StringLength(7)]
        public string COD_CLIENTE { get; set; }

        public bool? OPERACAO_TRIANGULAR { get; set; }
    }
}
