using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dalutex.Models.DataModels
{
    [Table("TI_DALUTEX.PED_LINK_QUANTD_TIPO")]
    public partial class PED_LINK_QUANTD_TIPO
    {
        [Key]
        [Column(Order = 1)]
        public int ID_GRUPO_COL { get; set; }
        [Key]
        [Column(Order = 2)]
        public int TIPO_PEDIDO { get; set; }
        [Key]
        [Column(Order = 3)]
        public int ID_TECNOLOGIA { get; set; }
        [Key]
        [Column(Order = 4)]
        public string ARTIGO { get; set; }
        public decimal QTDE_MIN { get; set; }
        public decimal QTDE_MAX { get; set; }
    }
}