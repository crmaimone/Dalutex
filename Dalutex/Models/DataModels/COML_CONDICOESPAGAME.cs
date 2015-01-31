namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DALUTEX.COML_CONDICOESPAGAME")]
    public partial class COML_CONDICOESPAGAME
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CONDICAO { get; set; }

        public int? SITUACAO { get; set; }

        [StringLength(40)]
        public string DESCRICAO { get; set; }

        public int? PARCELAS { get; set; }

        public int? PERCENTUALACUMULADO { get; set; }

        public int? DIASINICIO { get; set; }

        public int? INTERVALODIAS { get; set; }

        public int? PRAZOMEDIO { get; set; }

        [StringLength(5)]
        public string CODIGOALTERNATIVO { get; set; }

        public int? NECESSITAANALISE { get; set; }

        [Column(TypeName = "float")]
        public decimal? DESCONTOVINCULADO { get; set; }

        [Column(TypeName = "float")]
        public decimal? ACRESCIMOVINCULADO { get; set; }

        [StringLength(1)]
        public string CONDICAOANTECIPADA { get; set; }

        [Column(TypeName = "float")]
        public decimal? VALOR_DESDOBRAMENTO { get; set; }

        public byte? TIFORMAPAGAM { get; set; }
    }
}
