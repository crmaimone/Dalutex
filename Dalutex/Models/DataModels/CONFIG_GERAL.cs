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
        public int id_config { get; set;}
        public string rotina { get; set; }
        public string ativo { get; set;}
        public string boleano1 { get; set;}
        public string boleano2 { get; set;}
        public string boleano3 { get; set; }
        public string boleano4 { get; set;}
        public string boleano5{ get; set;}
        public string boleano6{ get; set;}
        public string boleano7{ get; set;}
        public string boleano8{ get; set;}
        public string boleano9{ get; set;}
        public string boleano10{ get; set;}
        public string parametro1{ get; set;}
        public string parametro2{ get; set;}
        public string parametro3{ get; set;}
        public string parametro4{ get; set;}
        public string parametro5{ get; set;}
        public string parametro6{ get; set;}
        public string parametro7{ get; set;}
        public string parametro8{ get; set;}
        public string parametro9{ get; set;}
        public string parametro10{ get; set;}
        public int? data1{ get; set;}
        public int? data2{ get; set;}
        public int? int1 { get; set; }
    }
}