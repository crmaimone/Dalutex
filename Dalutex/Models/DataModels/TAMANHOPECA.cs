namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.TAMANHOPECA")]
    public partial class TAMANHOPECA
    {
        [Key]
        [StringLength(6)]
        public string CODIGO { get; set; }

        [StringLength(60)]
        public string DESCRICAO { get; set; }
    }
}
