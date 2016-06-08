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

    [Table("TI_DALUTEX.VW_ARTIGOS_INATIVOS")]
    public partial class VW_ARTIGOS_INATIVOS
    {
       [Key]
       [Column(Order = 1)]      
       public string ARTIGO { get; set; }
    }
}