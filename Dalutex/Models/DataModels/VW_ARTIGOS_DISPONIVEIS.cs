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

    [Table("TI_DALUTEX.VW_ARTIGOS_DISPONIVEIS")]
    public partial class VW_ARTIGOS_DISPONIVEIS
    {
        [Key]
        [Column(Order=1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? ID_CARAC_TEC { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? ID_TECNOLOGIA { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ARTIGO { get; set; }

        public string TECNOLOGIA { get; set; }

        public override string ToString()
        {
            return this.ARTIGO + " " + this.TECNOLOGIA;
        }
    }
}