namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DALUTEX.COML_VIASTRANSPORTE")]
    public partial class COML_VIASTRANSPORTE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VIATRANSPORTE { get; set; }

        [StringLength(25)]
        public string DESCRICAO { get; set; }

        public int? CDSEFAZ { get; set; }

        [StringLength(1)]
        public string STINFORMAAFRMM { get; set; }
    }
}
