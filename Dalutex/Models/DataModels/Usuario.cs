namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("USUARIO")]
    public partial class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int USU_ID { get; set; }

        [StringLength(256)]
        public string NM_USU { get; set; }

        [StringLength(24)]
        public string USU_PWD { get; set; }

        [StringLength(32)]
        public string USU_LOGIN { get; set; }

        public int? USU_REP { get; set; }
    }
}
