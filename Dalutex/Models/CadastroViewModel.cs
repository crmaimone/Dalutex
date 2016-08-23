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

    public class PesquisaQualidadeComercialViewModel
    {
        public string QualidadeSelecionada { get; set; }
        public List<KeyValuePair<string,string>> Qualidades { get; set; }
    }

    public class PesquisaMoedaViewModel
    {
        public string MoedaSelecionada { get; set; }
        public List<CADASTRO_MOEDAS> Moedas { get; set; }
    }
    public class PesquisaCondicaoPagamentoViewModel
    {
        public string CondicaoSelecionada { get; set; }
        public List<VW_CONDICAO_PGTO> Condicoes { get; set; }
    }
    public class PesquisaCanalVendasViewModel
    {
        public string CanalSelecionado { get; set; }
        public List<CANAIS_VENDA> Canais { get; set; }
    }
    public class PesquisaViaTransporteViewModel
    {
        public string ViaSelecionada { get; set; }
        public List<COML_VIASTRANSPORTE> ViasTransporte { get; set; }
    }
    public class PesquisaFreteViewModel
    {
        public string FreteSelecionado { get; set; }
        public List<COML_TIPOSFRETE> Fretes { get; set; }
    }
    public class PesquisaTipoAtendimentoViewModel
    {
        public string TipoSelecionado { get; set; }
        public List<PRE_PEDIDO_ATEND> TiposAtendimento { get; set; }
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


    public class RascunhoPedidoViewModel
    {
        [Display(Name = "Pedido")]
        public string FiltroPedido { get; set; }
        [Display(Name = "Cliente")]
        public string FiltroCliente { get; set; }
        [Display(Name = "Representante")]
        public string FiltroRepresentante { get; set; }
        [Display(Name = "Filtrar datas?")]
        public bool FiltroData { get; set; }
        [Display(Name = "De")]
        public DateTime FiltroDataInicial { get; set; }
        [Display(Name = "Até")]
        public DateTime FiltroDataFinal { get; set; }

        public List<VW_RASCUNHO_PEDIDOS> Pedidos { get; set; }

        public int Pagina { get; set; }
        public int TotalPaginas { get; set; }
    }

    public class TabelaPrecosViewModel
    {
    }

}