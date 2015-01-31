namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DALUTEX.CADASTRO_MOEDAS")]
    public partial class CADASTRO_MOEDAS
    {
        [Key]
        public byte CODIGOMOEDA { get; set; }

        [StringLength(20)]
        public string NOMEMOEDA { get; set; }

        public short? CDSISCOMEX { get; set; }

        [StringLength(5)]
        public string SIGLA { get; set; }
    }
}
