namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.CONTROLE_DESENV_CARAC_TEC")]
    public partial class CONTROLE_DESENV_CARAC_TEC
    {
        [Key]
        public decimal ID_CARAC_TEC { get; set; }

        [StringLength(60)]
        public string CARACT_TECNICA { get; set; }
    }
}
