using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dalutex.Models.DataModels;

namespace Dalutex.Models
{
    public class PedidoViewModel
    {
        public List<ItensEstoque> Galeria { get; set; }
        public string UrlImagens { get; set; }
    }
}