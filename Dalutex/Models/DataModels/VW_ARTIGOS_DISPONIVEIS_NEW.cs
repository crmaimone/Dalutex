namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.VW_ARTIGOS_DISPONIVEIS_NEW")]
    public partial class VW_ARTIGOS_DISPONIVEIS_NEW
    {
        [Key]
        public decimal ID_DA_VIEW { get; set; }
      
        [StringLength(4)]
        public string ARTIGO { get; set; }

        [StringLength(50)]
        public string TECNOLOGIA { get; set; }

        public decimal? ID_CARAC_TEC { get; set; }

        public int? ID_TECNOLOGIA { get; set; }

        [StringLength(60)]
        public string CARACT_TECNICA { get; set; }

        [StringLength(4)]
        public string ART_DISP_PCP { get; set; }

        public decimal REST_DES { get; set; }

        public string COR { get; set; }
    }
}
