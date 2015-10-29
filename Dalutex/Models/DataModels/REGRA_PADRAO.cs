namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.REGRA_PADRAO")]
    public partial class REGRA_PADRAO
    {
        [Key]
        public decimal ID_REGRA { get; set; }

        [StringLength(1)]
        public string TIPO_PRODUTO { get; set; }

        [StringLength(1)]
        public string TECNOLOGIA { get; set; }

        [StringLength(4)]
        public string ARTIGO { get; set; }

        public decimal? ID_PROCESSO { get; set; }

        public decimal? REDUZIDO { get; set; }

        public decimal? VALOR_PADRAO { get; set; }

        public decimal? ID_USUARIO { get; set; }

        public DateTime? DT_REGRA { get; set; }

        public DateTime? VALIDADE_INI { get; set; }

        public DateTime? VALIDADE_FIM { get; set; }

        [StringLength(2)]
        public string UM { get; set; }

        public bool? STATUS { get; set; }

        public DateTime? DT_STATUS { get; set; }

        public decimal? ID_USUARIO_STATUS { get; set; }

        public bool? PADRAO { get; set; }
    }
}
