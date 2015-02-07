namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DALUTEX.COML_CONTATOS")]
    public partial class COML_CONTATOS
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDPESSOAFJ { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(25)]
        public string CONTATO { get; set; }

        [StringLength(25)]
        public string CARGO { get; set; }

        [StringLength(30)]
        public string TELEFONE { get; set; }

        [StringLength(10)]
        public string RAMAL { get; set; }

        [StringLength(50)]
        public string EMAIL { get; set; }

        [StringLength(30)]
        public string CELULAR { get; set; }
    }
}
