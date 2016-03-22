namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.ACOES")]
    public partial class ACOES
    {
        [Key]
        public decimal ID_ACAO { get; set; }

        [StringLength(60)]
        public string DESCRICAO_ACAO { get; set; }

        [StringLength(40)]
        public string UNIT { get; set; }
    }
}
