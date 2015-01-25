using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.CONFIG_GERAL")]
    public partial class CONFIG_GERAL
    {
        [Key]
        public int ID_CONFIG { get; set;}
        public string ROTINA { get; set; }
        public string ATIVO { get; set;}
        public string BOLEANO1 { get; set;}
        public string BOLEANO2 { get; set;}
        public string BOLEANO3 { get; set; }
        public string BOLEANO4 { get; set;}
        public string BOLEANO5{ get; set;}
        public string BOLEANO6{ get; set;}
        public string BOLEANO7{ get; set;}
        public string BOLEANO8{ get; set;}
        public string BOLEANO9{ get; set;}
        public string BOLEANO10{ get; set;}
        public string PARAMETRO1{ get; set;}
        public string PARAMETRO2{ get; set;}
        public string PARAMETRO3{ get; set;}
        public string PARAMETRO4{ get; set;}
        public string PARAMETRO5{ get; set;}
        public string PARAMETRO6{ get; set;}
        public string PARAMETRO7{ get; set;}
        public string PARAMETRO8{ get; set;}
        public string PARAMETRO9{ get; set;}
        public string PARAMETRO10{ get; set;}
        public int? DATA1{ get; set;}
        public int? DATA2{ get; set;}
        public int? INT1 { get; set; }
    }
}