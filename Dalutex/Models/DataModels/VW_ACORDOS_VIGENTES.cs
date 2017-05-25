namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.VW_ACORDOS_VIGENTES")]
    public partial class VW_ACORDOS_VIGENTES
    {
        [Key]
        [Column(Order = 0)]        
        public decimal? ID_REG { get; set; }
                
        public decimal ID_ACORDO_COMERCIAL { get; set; }

        public decimal? CLIENTE { get; set; }

        [StringLength(30)]
        public string NOME_CLIENTE { get; set; }

        [StringLength(100)]
        public string GRUPO_CLIENTE { get; set; }

        [StringLength(7)]
        public string ID_REP { get; set; }

        [StringLength(30)]
        public string REPRESENTANTE { get; set; }

        public DateTime DATA_INI_VIGENCIA { get; set; }

        public DateTime DATA_FIN_VIGENCIA { get; set; }

        public decimal? USUARIO_DIGITADOR { get; set; }

        public DateTime DATA_DIGITACAO { get; set; }

        public decimal? USUARIO_APROVACAO { get; set; }

        public DateTime? DATA_APROVACAO { get; set; }

        [StringLength(100)]
        public string OBSERVACAO_APROVACAO { get; set; }

        [StringLength(100)]
        public string OBSERVACAO_DIGITACAO { get; set; }

        public decimal ID_ITEM_ACORDO_COM { get; set; }

        public decimal ID_ACORDO { get; set; }

        [StringLength(10)]
        public string ARTIGO { get; set; }

        [StringLength(10)]
        public string DESENHO { get; set; }

        [StringLength(10)]
        public string VARIANTE { get; set; }

        [StringLength(10)]
        public string COR { get; set; }

        public decimal QUANTIDADES { get; set; }

        [StringLength(2)]
        public string UM { get; set; }

        public decimal PRECO_UNITARIO { get; set; }

        [StringLength(25)]
        public string USUARIO_DIG { get; set; }

        [StringLength(25)]
        public string USUARIO_APROV { get; set; }

        public decimal QTDE_DISPONIVEL { get; set; }        
    }
}
