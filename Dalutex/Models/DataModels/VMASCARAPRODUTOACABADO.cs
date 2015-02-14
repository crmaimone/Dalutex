namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DALUTEX.VMASCARAPRODUTOACABADO")]
    public partial class VMASCARAPRODUTOACABADO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CODIGO_REDUZIDO { get; set; }

        [StringLength(30)]
        public string COD_COMERCIAL { get; set; }

        [StringLength(4)]
        public string TIPO { get; set; }

        [StringLength(4)]
        public string NEGOCIO { get; set; }

        [StringLength(4)]
        public string PROCESSO { get; set; }

        [StringLength(4)]
        public string TIPO_ARTIGO { get; set; }

        [StringLength(4)]
        public string CLASSIF_COR { get; set; }

        [StringLength(4)]
        public string MAQUINA { get; set; }

        [StringLength(16)]
        public string ARTIGO { get; set; }

        [StringLength(28)]
        public string COR { get; set; }

        [StringLength(4)]
        public string EXCL { get; set; }

        [StringLength(16)]
        public string DESENHO { get; set; }

        [StringLength(8)]
        public string VARIANTE { get; set; }

        [StringLength(40)]
        public string ARTIGO_DESENHO_VAR { get; set; }

        [StringLength(32)]
        public string ARTIGO_DESENHO { get; set; }

        [StringLength(24)]
        public string DESENHO_VAR { get; set; }

        [StringLength(2)]
        public string UM { get; set; }

        public int? COLECAO { get; set; }

        public int? IMAGEM { get; set; }

        public short? LINHA_PRODUTO { get; set; }

        [StringLength(4)]
        public string CILIND_QUADRO { get; set; }

        [StringLength(60)]
        public string DESCRICAO { get; set; }

        [StringLength(4)]
        public string QUADR_OVER { get; set; }
    }
}
