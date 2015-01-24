namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.PED_ARTIGO_ATIVO")]
    public partial class PED_ARTIGO_ATIVO
    {
        [Key]
        [StringLength(4)]
        public string ARTIGO { get; set; }

        public bool? ID_PROCESSO { get; set; }

        public bool? ATIVO { get; set; }

        public virtual PED_ARTIGO_PROCESSO PED_ARTIGO_PROCESSO { get; set; }
    }
}
