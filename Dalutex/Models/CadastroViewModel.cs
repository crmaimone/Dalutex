using Dalutex.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dalutex.Models
{
    public class PesquisaRepresentantesViewModel{
        public string Filtro { get; set; }
        public List<REPRESENTANTES> Representantes { get; set; }
    }

    public class PesquisaClientesFaturaViewModel
    {
        public string Filtro { get; set; }
        public List<VW_CLIENTE_TRANSP> ClientesFatura { get; set; }
    }
}