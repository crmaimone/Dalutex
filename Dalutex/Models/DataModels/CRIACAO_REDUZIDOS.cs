namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.CRIACAO_REDUZIDOS")]
    public partial class CRIACAO_REDUZIDOS
    {
        [Key]
        public decimal ID_CRIACAO_REDUZIDOS { get; set; }

        [StringLength(4)]
        public string ARTIGO { get; set; }

        [StringLength(4)]
        public string DESENHO { get; set; }

        [StringLength(2)]
        public string VARIANTE { get; set; }

        [StringLength(1)]
        public string MAQUINA { get; set; }

        public DateTime? DATA_SOLICITACAO { get; set; }

        public DateTime? DATA_CRIACAO { get; set; }

        public decimal? REDUZIDO_CRIADO { get; set; }
    }
}
