namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.CONTROLE_DESENV_TECNOLOGIA")]
    public partial class CONTROLE_DESENV_TECNOLOGIA
    {
        [Key]
        public decimal ID_TEC { get; set; }

        [Required]
        [StringLength(50)]
        public string DESC_TEC { get; set; }
    }
}
