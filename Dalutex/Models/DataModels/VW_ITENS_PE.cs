namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.VW_ITENS_PE")]
    public partial class VW_ITENS_PE
    {                    
        [Key]
        [Column(Order = 0)]
        public decimal REDUZIDO { get; set; }
        [StringLength(20)]//8
        public string TECNOLOGIA { get; set; }
        [StringLength(4)]
        public string ARTIGO { get; set; }
        [StringLength(4)]        
        public string DESENHO { get; set; }
        [StringLength(7)]  
        public string COR { get; set; }
        [StringLength(2)]  
        public string VARIANTE { get; set; }
        [StringLength(2000)]  
        public string COMPOSICAO { get; set; }
        [StringLength(30)]  
        public string FAMILIA { get; set; }
        [StringLength(30)]  
        public string COLECAO { get; set; }       
        public decimal? PRIM_QL_KG { get; set; }
        public decimal? PRIM_QL_MT { get; set; }
        public decimal? SEG_QL_KG { get; set; }
        public decimal? SEG_QL_MT { get; set; }
        public decimal? TERC_QL_KG { get; set; }        
        public decimal? TERC_QL_MT { get; set; }
        public decimal? MTS { get; set; }
        public decimal? KGS { get; set; }
    }
}
