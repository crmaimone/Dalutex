using Dalutex.Models.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int IDTipoPedido { get; set; }
    }

    public class PesquisaClientesEntregaViewModel
    {
        public string Filtro { get; set; }
        public List<VW_CLIENTE_TRANSP> ClientesEntrega { get; set; }
    }

    public class PesquisaTransportadoraViewModel
    {
        public string Filtro { get; set; }
        public List<TRANSPORTADORAS> Transportadoras { get; set; }
    }


    public class DesenhosSemImagemModelView
    {
        public int Pagina { get; set; }
        public int TotalPaginas { get; set; }
        public List<ItemReserva> Galeria { get; set; }
        public string UrlImagens { get; set; }
        [Display(Name = "Código de studio:")]
        public string FiltroCodStudio { get; set; }
        [Display(Name = "Código DAL:")]
        public string FiltroCodDal { get; set; }
        [Display(Name = "Desenho:")]
        public string FiltroDesenho { get; set; }
        [Display(Name = "Studio:")]
        public string FiltroStudio { get; set; }
    }

    public class UploadImageModelView
    {
        public string Studio { get; set; }       
        public string CodStudio { get; set; }
        public string CodDal { get; set; }
        public string Desenho { get; set; }
    }

}