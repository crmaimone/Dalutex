namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.VW_GRUPO_CLIENTE_OU_CLIENTE")]
    public partial class VW_GRUPO_CLIENTE_OU_CLIENTE
    {
        [Key]
        [Column(Order = 0)]
        public int IDPESSOAFJ { get; set; }

        public decimal ID_GRUPO { get; set; }

        [StringLength(100)]
        public string GRUPO { get; set; }

        public decimal ID_CLIENTE { get; set; } 
    }
}
