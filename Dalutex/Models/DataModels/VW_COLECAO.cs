namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.VW_COLECAO")]
    public partial class VW_COLECAO
    {
        public DateTime? LANCAMENTO { get; set; }

        public DateTime? VIGENCIA { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_COLECAO { get; set; }

        [StringLength(30)]
        public string NOME_COLECAO { get; set; }
    }
}
