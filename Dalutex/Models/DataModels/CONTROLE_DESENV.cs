namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.CONTROLE_DESENV")]
    public partial class CONTROLE_DESENV
    {
        [Key]
        public decimal ID_CONTROLE_DESENV { get; set; }

        [StringLength(10)]
        public string ID_CLIENTE { get; set; }

        [StringLength(50)]
        public string CLIENTE_NOVO { get; set; }

        public decimal? ID_STUDIO { get; set; }

        [StringLength(20)]
        public string COD_STUDIO { get; set; }

        [StringLength(50)]
        public string NOME_STUDIO { get; set; }

        public DateTime? DT_ENT_CRIACAO { get; set; }

        public DateTime? DT_SAI_CRIACAO { get; set; }

        public decimal? STATUS_CRIACAO { get; set; }

        [StringLength(200)]
        public string OBSERVACAO_CRIACAO { get; set; }

        public decimal? ID_DESENHISTA_ART { get; set; }

        public DateTime? DT_ENT_ART { get; set; }

        public DateTime? DT_SAI_ART { get; set; }

        [StringLength(10)]
        public string DESENHO { get; set; }

        [StringLength(50)]
        public string NOME_DESENHO { get; set; }

        public decimal? STATUS_ART { get; set; }

        [StringLength(200)]
        public string OBSERVACAO_ART { get; set; }

        public decimal? ID_COLORISTA { get; set; }

        public DateTime? DT_ENVIO { get; set; }

        public decimal? STATUS_ENVIO { get; set; }

        [StringLength(200)]
        public string OBSERVACAO_ENVIO { get; set; }

        public decimal? STATUS_GERAL { get; set; }

        public DateTime? DT_RECEB_AMOSTRA { get; set; }

        public DateTime? DT_CANCELAMENTO { get; set; }

        [StringLength(200)]
        public string MOTIVO_CANCELAMENTO { get; set; }

        [StringLength(50)]
        public string USUARIO_CANCELAMENTO { get; set; }

        [StringLength(1)]
        public string TEM_CRIACAO { get; set; }

        public decimal? ID_ITEM_STUDIO { get; set; }

        public decimal? ID_REP { get; set; }

        public decimal? ID_TEMA { get; set; }

        public decimal? ID_DESENHO1 { get; set; }

        public decimal? ID_DESENHO2 { get; set; }

        public decimal? ID_TECNOLOGIA { get; set; }

        public decimal? FATURAR { get; set; }

        public DateTime? DT_ENT_ATEND { get; set; }

        public DateTime? DT_SAI_ATEND { get; set; }

        [StringLength(200)]
        public string OBSERVACAO_ATEND { get; set; }

        public decimal? ID_USUARIO { get; set; }

        public decimal? ORIGEM { get; set; }

        public DateTime? DT_ULTIMA_ALT_ATT { get; set; }

        [StringLength(50)]
        public string USUARIO_APROV_CANCEL { get; set; }

        public DateTime? DT_APROV_CANCEL { get; set; }

        [StringLength(200)]
        public string MOTIVO_APROV_CANCEL { get; set; }
    }
}
