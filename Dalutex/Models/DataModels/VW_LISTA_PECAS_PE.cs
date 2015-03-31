namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.VW_LISTA_PECAS_PE")]
    public partial class VW_LISTA_PECAS_PE
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NUMERO_PECA { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short SEQUENCIA { get; set; }

        [Column(TypeName = "float")]
        public decimal REDUZIDO { get; set; }

        public int QUALIDADE { get; set; }

        [Column(TypeName = "float")]
        public decimal PESO_PECA { get; set; }

        [Column(TypeName = "float")]
        public decimal METROS_PECA { get; set; }
    }
}
