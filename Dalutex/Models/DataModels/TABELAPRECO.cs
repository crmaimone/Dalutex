namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.TABELAPRECO")]
    public partial class TABELAPRECO
    {
        public DateTime DATAINICIO { get; set; }

        public DateTime? DATAFIM { get; set; }

        [Required]
        [StringLength(60)]
        public string NOME { get; set; }

        [StringLength(255)]
        public string DESCRICAO { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
    }
}
