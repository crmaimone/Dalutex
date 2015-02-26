namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.VW_CLIENTE_TRANSP")]
    public class VW_CLIENTE_TRANSP
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 1)]
        public int ID_CLIENTE { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 2)]
        public int ID_REP { get; set; }
        public int ID_TRANSP { get; set; }
        public string NOME { get; set; }      
        public string CNPJ { get; set; }
        public string UF { get; set; }
        public string ENDERECO { get; set; }
        public string BAIRRO { get; set; }
        public string CIDADE { get; set; }
        public string CEP { get; set; }
        public string DESPACHO { get; set; }
        public string TRANSPORTADORA { get; set; }

    }
}