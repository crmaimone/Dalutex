namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.VW_GRUPO_COL_RED")]
    public partial class VW_GRUPO_COL_RED
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int REDUZIDO { get; set; }

        public decimal? TIPO_COL { get; set; }

        public int ID_COL { get; set;  }
        public int ID_GRUPO_COL { get; set; }
    }
}
