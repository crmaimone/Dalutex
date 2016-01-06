namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.VW_EMAILS")]
    public partial class VW_EMAILS
    {
        [Key]
        [Column(Order = 0)]
        public decimal CLIENTESGT { get; set; }
        [Key]
        [Column(Order = 1)]
        public decimal IDREPRESENTANTE { get; set; }

        public string EMAIL_REP { get; set; }
        public string EMAIL_CLIENTE { get; set; }
    }
}