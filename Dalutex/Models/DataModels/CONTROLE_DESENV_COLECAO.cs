namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.CONTROLE_DESENV_COLECAO")]
    public partial class CONTROLE_DESENV_COLECAO
    {
        public decimal? REDUZIDO { get; set; }

        public decimal? COLECAO { get; set; }

        public decimal? USUARIO { get; set; }

        public DateTime? DATA_INCLUSAO { get; set; }

        public decimal? PEDIDO { get; set; }

        [StringLength(100)]
        public string OCORRENCIA { get; set; }

        [StringLength(10)]
        public string ARTIGO { get; set; }

        [StringLength(10)]
        public string DESENHO { get; set; }

        [StringLength(10)]
        public string VARIANTE { get; set; }

        [StringLength(10)]
        public string TECNOLOGIA { get; set; }

        [StringLength(10)]
        public string COR { get; set; }

        [Key]
        public decimal ID_CONTROLE_DESENV_COLECAO { get; set; }

        public DateTime? DATAHORA_OCORR { get; set; }

        public decimal? ID_USUARIO_OCORR { get; set; }

        [StringLength(10)]
        public string ARTIGO_OCORRENCIA { get; set; }
    }
}
