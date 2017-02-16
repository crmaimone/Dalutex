namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.VW_LISOS_POR_COLECAO")]
    public partial class VW_LISOS_POR_COLECAO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CODIGO_REDUZIDO { get; set; }

        public int COLECAO { get; set; }

        public string ARTIGO { get; set; }

        [StringLength(28)]
        public string COR { get; set; }

        [StringLength(254)]
        public string CAMINHO { get; set; }

        public string ARTIGO_ATIVO { get; set; }
    }
}
