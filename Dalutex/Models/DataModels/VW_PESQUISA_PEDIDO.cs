using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dalutex.Models.DataModels
{
    [Table("TI_DALUTEX.VW_PESQUISA_PEDIDO")]
    public class VW_PESQUISA_PEDIDO
    {
        [Key]
        public decimal PEDIDO { get; set; }
        public string REPRESENTANTE { get; set; }
        public string CLIENTE { get; set; }
        public DateTime DATA_EMISSAO { get; set; }
        public decimal STATUS_PEDIDO { get; set; }
    }
}