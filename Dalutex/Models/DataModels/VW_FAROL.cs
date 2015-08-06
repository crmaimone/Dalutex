namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.VW_FAROL")]
    public partial class VW_FAROL
    {
        [Key]
        [Column(Order = 0)]
        public string REDUZIDO { get; set; }
             
        public decimal FAROL { get; set; }

        [StringLength(4)]
        public string ARTIGO { get; set; }

        [StringLength(4)]
        public string DESENHO { get; set; }

        [StringLength(2)]
        public string VARIANTE { get; set; }
    }
}
