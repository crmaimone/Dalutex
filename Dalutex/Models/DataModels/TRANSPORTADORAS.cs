namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DALUTEX.TRANSPORTADORAS")]
    public partial class TRANSPORTADORAS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDTRANSPORTADORA { get; set; }

        [StringLength(25)]
        public string NOME { get; set; }

        public int? IDPESSOAFJ { get; set; }

        public short? VIATRANSPORTE { get; set; }

        [StringLength(30)]
        public string CIDADE { get; set; }

        public int? IDCIDADE { get; set; }

        [StringLength(1)]
        public string STEXIGRETEICMSTRAN { get; set; }
    }
}
