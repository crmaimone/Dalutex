namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DALUTEX.REPRESENTANTES")]
    public partial class REPRESENTANTES
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDREPRESENTANTE { get; set; }

        [StringLength(30)]
        public string NOME { get; set; }

        public int? IDPESSOAFJ { get; set; }

        public short? QTDAMOSTRAS { get; set; }

        public int? GERENTE { get; set; }

        [Column(TypeName = "float")]
        public decimal? COMISSAONAAREA { get; set; }

        [Column(TypeName = "float")]
        public decimal? COMISSAOFORAAREA { get; set; }

        public short? ADMINISTRADOR { get; set; }

        public int? IDAREA { get; set; }

        public int? IDUEN { get; set; }

        public short? REGIAO { get; set; }

        public byte? SITUACAO { get; set; }

        [Column(TypeName = "float")]
        public decimal? PERCCOMISSAOFATURA { get; set; }

        [Column(TypeName = "float")]
        public decimal? PERCCOMISSAOBAIXA { get; set; }

        public int? CODREDUSUARIO { get; set; }
    }
}
