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

    [Table("TI_DALUTEX.CONTROLE_DESENV_LINK_CT")]
    public partial class CONTROLE_DESENV_LINK_CT
    {
        public int? ID_ITEM_STUDIO { get; set; }

        public int? ID_CARAC_TEC { get; set; }
    }
}