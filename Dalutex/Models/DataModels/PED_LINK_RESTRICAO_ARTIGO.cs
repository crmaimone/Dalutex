using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.PED_LINK_RESTRICAO_ARTIGO")]
    public partial class PED_LINK_RESTRICAO_ARTIGO
    {
        [Key]
        [Column(Order = 1)]    
        public int? ID_CARAC_TEC { get; set; }

        [Key]
        [Column(Order = 2)]
        public int? ID_TECNOLOGIA { get; set; }

        [StringLength(4)]
        public string ARTIGO { get; set; }
    }
}