namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DALUTEX.CANAIS_VENDA")]
    public partial class CANAIS_VENDA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short CANAL_VENDA { get; set; }

        [StringLength(40)]
        public string DESCRICAO { get; set; }

        [StringLength(1)]
        public string GERAROMANEIOSEPAUTOM { get; set; }

        public byte? FILIAL { get; set; }

        public byte? SITUACAO { get; set; }
    }
}
