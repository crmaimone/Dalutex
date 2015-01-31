namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DALUTEX.COML_TIPOSFRETE")]
    public partial class COML_TIPOSFRETE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TIPOFRETE { get; set; }

        [StringLength(25)]
        public string DESCRICAO { get; set; }

        [Column(TypeName = "float")]
        public decimal? VALORDESDOBRAMENTO { get; set; }
    }
}
