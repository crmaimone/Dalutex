namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DALUTEX.COML_TIPOSPEDIDOS")]
    public partial class COML_TIPOSPEDIDOS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TIPOPEDIDO { get; set; }

        [StringLength(25)]
        public string DESCRICAO { get; set; }

        public int? COMISSAOZERADA { get; set; }

        [StringLength(1)]
        public string STEXIGEREPRESENTANTE { get; set; }
    }
}
