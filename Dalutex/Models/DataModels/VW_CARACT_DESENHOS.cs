namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.VW_CARACT_DESENHOS")]
    public partial class VW_CARACT_DESENHOS
    {                            
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public string TECNOLOGIA { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? ID_CARAC_TEC { get; set; } 

        public string TECNOLOGIA { get; set; }

        [StringLength(4)]
        public string DESENHO { get; set; }

        public string CARACT_TECNICA { get; set; }

        public int? ID_TECNOLOGIA { get; set; }         
    }
}
