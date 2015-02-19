namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.VW_CONDICAO_PGTO")]
    public partial class VW_CONDICAO_PGTO
    {
        [Key]
        public int ID_COND { get; set; }

        [StringLength(40)]
        public string DESCRI_COND { get; set; }

        public int PARCELAS { get; set; }
    }
}
