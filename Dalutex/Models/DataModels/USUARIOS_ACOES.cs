namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.USUARIOS_ACOES")]
    public partial class USUARIOS_ACOES
    {
        [Key]
        [Column(Order = 0)]
        public decimal ID_USUARIO { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal ID_ACAO { get; set; }
    }
}
