namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.EMAIL_TABELA_PRECO")]
    public partial class EMAIL_TABELA_PRECO
    {
        [Key]
        public decimal ID_EMAIL_TABELA_PRECO { get; set; }

        public decimal? ID_UAUARIO { get; set; }

        public decimal? STATUS_ENVIO { get; set; }

        public DateTime? DATA_ENVIO { get; set; }
    }
}
