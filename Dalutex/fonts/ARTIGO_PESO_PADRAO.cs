namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.ARTIGO_PESO_PADRAO")]
    public partial class ARTIGO_PESO_PADRAO
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(4)]
        public string ARTIGO { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(1)]
        public string TECNOLOGIA { get; set; }

        [StringLength(2)]
        public string UM { get; set; }

        [Key]
        [Column(Order = 2)]
        public decimal VALOR { get; set; }

        [StringLength(50)]
        public string USUARIO { get; set; }

        public DateTime? DT_ATIVA { get; set; }

        public bool? ATIVO { get; set; }
    }
}
