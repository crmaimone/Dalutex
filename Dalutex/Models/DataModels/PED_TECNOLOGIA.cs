namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.PED_TECNOLOGIA")]
    public partial class PED_TECNOLOGIA
    {
        [Key]
        public decimal ID_TECNOLOGIA { get; set; }

        [StringLength(50)]
        public string TECNOLOGIA { get; set; }
    }
}
