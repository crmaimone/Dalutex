namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.LINK_GRUPO_COND_PGTO")]
    public partial class LINK_GRUPO_COND_PGTO
    {
        [Key]
        public int ID_LINK_GRUPO_COND_PGTO { get; set; }

        public int COD_COND { get; set; }

        public int ID_GRUPO_COND { get; set; }
    }
}
