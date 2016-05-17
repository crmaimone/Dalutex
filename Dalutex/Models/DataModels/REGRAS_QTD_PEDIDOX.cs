namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.REGRAS_QTD_PEDIDOX")]
    public partial class REGRAS_QTD_PEDIDOX
    {
        [Key]
        public decimal ID_REGRAS_QTD_PEDIDO { get; set; }

        [StringLength(1)]
        public string TECNOLOGIA { get; set; }

        public decimal? PROCESSO { get; set; }

        [StringLength(1)]
        public string TECNOLOGIA_DESTINO { get; set; }

        public decimal? PROCESSO_DESTINO { get; set; }

        public decimal? GRUPO_COLECAO { get; set; }

        public decimal? TIPO_PEDIDO { get; set; }

        [StringLength(2)]
        public string UM { get; set; }

        public decimal QTD_MIN_VAR { get; set; }

        public decimal QTD_MAX_VAR { get; set; }

        public decimal QTD_MIN_DES { get; set; }

        public decimal QTD_MAX_DES { get; set; }

        public bool? EXCLUIDO { get; set; }

        public decimal? USUARIO_EXCLUSAO { get; set; }

        public DateTime? DATAHORA_EXCLUSAO { get; set; }

        public bool? TRAB_ARTE_FINAL { get; set; }
         
        [StringLength(3)]
        public string? COLUNA_EXCEL { get; set; }

    }
}
