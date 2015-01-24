namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.PED_ARTIGO_PROCESSO")]
    public partial class PED_ARTIGO_PROCESSO
    {
        public PED_ARTIGO_PROCESSO()
        {
            PED_ARTIGO_ATIVO = new HashSet<PED_ARTIGO_ATIVO>();
        }

        [Key]
        public bool ID_PROCESSO { get; set; }

        [StringLength(50)]
        public string PROCESSO { get; set; }

        public virtual ICollection<PED_ARTIGO_ATIVO> PED_ARTIGO_ATIVO { get; set; }
    }
}
