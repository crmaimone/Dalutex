namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.VW_EMAIL_USUARIO")]
    public partial class VW_EMAIL_USUARIO
    {
        [Key]
        public decimal ID_USUARIO { get; set; }

        [StringLength(50)]
        public string EMAIL { get; set; }
    }
}
