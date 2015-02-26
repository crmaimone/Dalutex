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

    [Table("TI_DALUTEX.VW_TROCA_TEC")]
    public partial class VW_TROCA_TEC
    {
        [Key]
        [Column(Order=1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? ID_TEC_NOVA { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? ID_TEC_ORIGINAL { get; set; }
    }
}
