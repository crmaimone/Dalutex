namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DALUTEX.COLECOES")]
    public partial class COLECOES
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int COLECAO { get; set; }

        [StringLength(4)]
        public string ID_COLECAO { get; set; }

        [StringLength(30)]
        public string NOME { get; set; }

        public int? LANCAMENTO { get; set; }

        public int? VIGENCIA { get; set; }

        public int? IDESTACAO { get; set; }
    }
}
