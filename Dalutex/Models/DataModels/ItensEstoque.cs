namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ITENS_ESTOQUE")]
    public partial class ItensEstoque
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDItemEstoque { get; set; }

        [Required]
        [StringLength(50)]
        public string Codigo_Reduzido { get; set; }

        [Required]
        [StringLength(100)]
        public string Codigo { get; set; }

        public int Colecao { get; set; }
    }
}
