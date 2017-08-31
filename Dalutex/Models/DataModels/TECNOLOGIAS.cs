namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.TECNOLOGIAS")]
    public partial class TECNOLOGIAS
    {
        [Key]
        public decimal ID_TEC { get; set; }

        [Required]
        [StringLength(50)]
        public string DESC_TEC { get; set; }

        [Required]
        [StringLength(2)]
        public string COD_ID { get; set; }

        [StringLength(50)]
        public string DESC_OUTRO_IDIOMA { get; set; }

        public bool? ATIVA_CUSTO { get; set; }
    }
}
