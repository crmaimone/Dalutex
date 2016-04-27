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

    [Table("TI_DALUTEX.VW_ORDEM_ANTERIOR_BLOQ_NEW")]
    public class VW_ORDEM_ANTERIOR_BLOQ_NEW
    {       
        [Key]
        [Column(Order = 0)]
        public string COD_DAL_MESTRE { get; set; }
    }

}