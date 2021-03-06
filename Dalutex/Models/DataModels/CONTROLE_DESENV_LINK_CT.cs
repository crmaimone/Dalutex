﻿using System;
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
        [Key]
        [Column(Order=1)]    
        public int? ID_ITEM_STUDIO { get; set; }

        [Key]
        [Column(Order = 2)]
        public int? ID_CARAC_TEC { get; set; }
    }
}