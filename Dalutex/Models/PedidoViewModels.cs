using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dalutex.Models.DataModels;
using System.ComponentModel.DataAnnotations;
using Dalutex.Models.Utils;
using System.Drawing;
using System.Web.Mvc;

namespace Dalutex.Models
{
    public class PreviewViewModel
    {
        public bool? Sucesso { get; set; }
        [Display(Name = "Representante:")]
        public string Representante { get; set; }
        [Display(Name = "Cliente da fatura:")]
        public string ClienteFatura { get; set; }
        [Display(Name = "Cliente da entrega:")]
        public string ClienteEntrega { get; set; }
        [Display(Name = "Transportadora:")]
        public string Transportadora { get; set; }

        [Display(Name = "Pedido:")]
        public string Pedido { get; set; }
        [Display(Name = "Tipo Atendimento:")]
        public string TipoAtendimento { get; set; }

        [Display(Name = "Observações:")]
        public string Observacoes { get; set; }
        [Display(Name = "Pedido do Cliente:")]
        public string PedidoDoCliente { get; set; }
        [Display(Name = "Tipo de Pedido:")]
        public string TipoPedido { get; set; }
        [Display(Name = "Frete:")]
        public string Frete { get; set; }
        [Display(Name = "Condição Pagto:")]
        public string CondPagto { get; set; }
        [Display(Name = "Qualidade Comercial:")]
        public string QualidadeCom { get; set; }



        public ConclusaoPedidoViewModel Carrinho { get; set; }
        public string UrlDesenhos { get; set; }
        public string UrlReservas { get; set; }
    }

    public class DesenhoVariante
    {
        public string Desenho { get; set; }
        public string Variante { get; set; }
    }
    
    public class ItemReserva
    {
        public string Desenho { get; set; }
        public string CodStudio { get; set; }
        public string CodDal { get; set; }
        public decimal? IDControleDesenvolvimento { get; set; }
        public int IDItemStudio { get; set; }
        public int IDStudio { get; set; }
        public string Studio { get; set; }
    }

    public class ArtigoTecnologia
    {
        public ArtigoTecnologia()
        {
            Restricao = "";
        }

        public string Artigo { get; set; }
        public string Tecnologia { get; set; }
        public bool TemNoCarrinho { get; set; }
        public string Restricao { get; set; }
    }


    public class ArtigosInativos
    {
        public string Artigo { get; set; }
    }

    public class Liso
    {
        public string Artigo { get; set; }
        public string Cor { get; set; }
        public string RGB { get; set; }
        public int Reduzido { get; set; }
        public bool TemNoCarrinho { get; set; }
    }

    public class DesenhosViewModel
    {
        public int IDColecao { get; set; }
        public string NMColecao { get; set; }
        public int Pagina { get; set; }
        public int TotalPaginas { get; set; }
        public List<DesenhoVariante> Galeria { get; set; }
        public string UrlImagens { get; set; }
        public string FiltroDesenho { get; set; }
        public string FiltroArtigo { get; set; }
        public string FiltroTecnologia { get; set; }
    }

    public class ItensProntaEntregaViewModel
    {
        public int Pagina { get; set; }
        public int TotalPaginas { get; set; }
        public List<VW_ITENS_PE> Galeria { get; set; }
        public string UrlImagens { get; set; }
        public string FiltroDesenho { get; set; }
        public Enums.ItemType Tipo { get; set; }
    }

    public class DetalhesPEViewModel
    {        
        public int Pagina { get; set; }
        public int TotalPaginas { get; set; }
        public List<VW_LISTA_PECAS_PE> ListaPecasPE { get; set; }
        public List<VW_ITENS_PE> DetalhesReduzido { get; set; }
        public string UrlImagens { get; set; }
        
        public int Reduzido { get; set; }
        public string Artigo { get; set; }
        public string Desenho { get; set; }
        public string Cor { get; set; }
        public string Variante { get; set; }
        public string Tecnologia { get; set; }
        public string Composicao { get; set; }
        public string Colecao { get; set; }
        public decimal KGPrimeira { get; set; }
        public decimal KGSegunda { get; set; }
        public decimal KGTerceira { get; set; }
        public decimal MTPrimeira { get; set; }
        public decimal MTSegunda { get; set; }
        public decimal MTTerceira { get; set; } 
        public decimal Largura { get; set; } 
        public decimal Gramatura { get; set; } 
        public decimal Rendimento { get; set; }
        public string UM { get; set; }
        public int IDColecao { get; set; }

        public Enums.ItemType Tipo { get; set; }
    }

    public class LisosViewModel
    {
        public int IDColecao { get; set; }
        public string NMColecao { get; set; }
        public int Pagina { get; set; }
        public int TotalPaginas { get; set; }
        public List<Liso> Galeria { get; set; }
        public string FiltroCor { get; set; }
        public string FiltroArtigo { get; set; }
        public string FiltroReduzido { get; set; }
    }

    public class ItensParaReservaViewModel
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
    }

    public class ValidaPedidoReservaViewModel
    {
        [Display(Name = "Pedido reserva:")]
        public string FiltroPedidoReserva { get; set; }

        [Display(Name = "Cliente:")]
        public string FiltroCliente { get; set; }

        [Display(Name = "Representante:")]
        public string FiltroRepresentante { get; set; }

        [Display(Name = "Cód. de studio:")]
        public string FiltroCodStudio { get; set; }

        [Display(Name = "Cód. Dal:")]
        public string FiltroCodDal { get; set; }

        [Display(Name = "Desenho:")]
        public string FiltroDesenho { get; set; }

        public int Pagina { get; set; }
        public int TotalPaginas { get; set; }
        public List<VW_VALIDAR_RESERVA> ListaValidaReserva { get; set; }
    }



    public class ReacabamentoViewModel
    {
        [Display(Name = "Reduzido")]
        public string FiltroReduzido { get; set; }

        [Display(Name = "Codigo")]
        public string FiltroCodigo { get; set; }

        [Display(Name = "Artigo")]
        public string FiltroArtigo { get; set; }

        [Display(Name = "Cor")]
        public string FiltroCor { get; set; }

        [Display(Name = "Tecnologia")]
        public string FiltroTecnologia { get; set; }

        [Display(Name = "Desenho")]
        public string FiltroDesenho { get; set; }

        [Display(Name = "Variante")]
        public string FiltroVariante { get; set; }

        public int Pagina { get; set; }
        public int TotalPaginas { get; set; }
        public List<VMASCARAPRODUTOACABADO> ListaItensReacabamento { get; set; }
    }



    public class DesenhosValidaReservaViewModel
    {    
        public int PedidoReserva { get; set; }

        public string IDCliente { get; set; }
    
        public int IDRepresentante { get; set; }

        public string CodStudio { get; set; }

        public string CodDal { get; set; }

        public string Desenho { get; set; }
        
        public int IDControle { get; set; }
        
        public string Variante { get; set; }
        
        public int IDVariante { get; set; }
        
        public int ItemPedidoRes { get; set; }

        public List<VW_ITENS_VALIDAR_RESERVA> Galeria { get; set; }

        public int Pagina { get; set; }

        public int TotalPaginas { get; set; }

        public string UrlImagens { get; set; }
    }

    public class ParametrosPreco
    {
        public bool E_Exclusivo { get; set; }
        public decimal Comissao  { get; set; }
        public int? IDColecao { get; set; }
    }

    public class MenuColecoesViewModel
    {
        public string Filtro { get; set; }
        public int Pagina { get; set; }
        public int TotalPaginas { get; set; }
        public List<VW_COLECAO> Colecoes { get; set; }
    }

    public class ArtigosDisponiveisViewModel
    {
        public string Desenho { get; set; }
        public string Variante { get; set; }
        public string Imagem { get; set; }
        public int IDColecao { get; set; }
        public List<ArtigoTecnologia> Artigos { get; set; }
        public string TecnologiaAtual { get; set; } 
        public string NMColecao { get; set; }
        public int Pagina { get; set; }

        public int PedidoReserva { get; set; }
        public int IDVariante { get; set; }
        public int ItemPedidoReserva { get; set; }
        public Enums.ItemType Tipo { get; set; }
        public int Reduzido { get; set; }

        //public List<ArtigosInativos> ArtigosInativos { get; set; }
    }

    public class InserirNoCarrinhoViewModel
    {
        public int ID { get; set; }
        public bool PreExistente { get; set; }
        public bool Excluir { get; set; }

        public InserirNoCarrinhoViewModel()
        {
            this.ComposeOpcoes = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };//Valores fixos de zero a dez. verificar necessidade de mais.
            this.Compose = 0;
            this.IDTamanhoPadrao = 0;
        }

        public int NumeroSequencial { get; set; }

        public string Desenho { get; set; }
        public string Variante { get; set; }
        public string Artigo { get; set; }
        public string TecnologiaOriginal { get; set; }
        public string TecnologiaPorExtenso { get; set; }
        public string Tecnologia { get { return this.TecnologiaPorExtenso != null ? this.TecnologiaPorExtenso.Substring(0, 1) : null; } }
        public string UnidadeMedida { get; set; }
        public decimal ValorPadrao { get; set; }
        public decimal ValorTotalItem { get; set; }
        public string ObterTipoPedido { get; set; }
        public int Reduzido { get; set; }

        [Display(Name = "DATA DISP.")]
        public DateTime DataEntregaItem { get; set; }

        [Display(Name = "DATA SOL.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DtItemSolicitada { get; set; }

        public int NumeroMaximoDiasDataSolicitacao { get; set; }

        public string Modo { get; set; } //I= Inclusão A=Alteração
        public Enums.ItemType Tipo { get; set; }

        public string IDColecao { get; set; }
        public string NMColecao { get; set; }
        public int Pagina { get; set; }
        public string Cor { get; set; }
        public string RGB { get; set; }
        
        public string CodStudio { get; set; }
        public string CodDal { get; set; }
        public int IDStudio { get; set; }
        public int IDItemStudio { get; set; }

        public int IDVariante { get; set; }
        public int PedidoReserva { get; set; }
        public int ItemPedidoReserva { get; set; }

        [Display(Name = "COMPOSE:")]
        public int Compose { get; set; }
        public List<int> ComposeOpcoes { get; set; }
        
        public int Comissao {get; set; } 

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "TIPO DE PEDIDO:")]
        public int IDTipoPedido { get; set; }
        public List<COML_TIPOSPEDIDOS> TiposPedido { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "TAMANHO PADRÃO:")]
        public decimal? IDTamanhoPadrao { get; set; }
        public List<REGRA_PADRAO> TamanhoPadrao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name="PEÇAS:")]
        public int Pecas { get; set; }

        [DataType(DataType.Currency)]
        public decimal Quantidade { get; set; }

        [DataType(DataType.Currency)]
        public decimal QuantidadeConvertida { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")] //oda -- format
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.Currency, ErrorMessage ="O valor {0} não é válido para {1}.")]
        [Display(Name = "PREÇO:")]        
        public decimal Preco { get; set; }

        public decimal? PrecoTabela { get; set; }
        public decimal Farol { get; set; }

        public int IDGrupoColecao { get; set; }

        public override bool Equals(object obj)
        {
            if(obj is InserirNoCarrinhoViewModel)
            {
                if ((obj as InserirNoCarrinhoViewModel).Tipo == Enums.ItemType.Estampado)
                {
                    return this.Artigo == (obj as InserirNoCarrinhoViewModel).Artigo
                            && this.Desenho == (obj as InserirNoCarrinhoViewModel).Desenho
                            && this.Variante == (obj as InserirNoCarrinhoViewModel).Variante; 
                }
                else if ((obj as InserirNoCarrinhoViewModel).Tipo == Enums.ItemType.Liso)
                {
                    return this.Reduzido == (obj as InserirNoCarrinhoViewModel).Reduzido;
                }
                else if ((obj as InserirNoCarrinhoViewModel).Tipo == Enums.ItemType.Reserva)
                {
                    return this.CodDal == (obj as InserirNoCarrinhoViewModel).CodDal;
                }
                else if ((obj as InserirNoCarrinhoViewModel).Tipo == Enums.ItemType.ValidacaoReserva)
                {
                    return this.Artigo == (obj as InserirNoCarrinhoViewModel).Artigo
                            && this.Desenho == (obj as InserirNoCarrinhoViewModel).Desenho
                            && this.Variante == (obj as InserirNoCarrinhoViewModel).Variante;
                }
                else
                {
                    return this.Reduzido == (obj as InserirNoCarrinhoViewModel).Reduzido;
                }
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool TemRestricao { get; set; }
        public string Restricao{ get; set; }

        public bool EhReacabamento { get; set; }
    }

    public class AmpliacaoViewModel
    {
        public string Desenho { get; set; }
        public string Variante { get; set; }
        public string Imagem { get; set; }
        public string IDColecao { get; set; }
        public string NMColecao { get; set; }
        public int Pagina { get; set; }
        public string RetornarPara { get; set; }
        public int Tipo { get; set; }
        public string CodStudio { get; set; }
        public string RGB { get; set; }
    }

    public class PesquisaPedidoViewModel
    {
        [Display(Name="Pedido")]
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

        public List<VW_PESQUISA_PEDIDO> Pedidos { get; set; }

        public int Pagina { get; set; }
        public int TotalPaginas { get; set; }
        public string TipoPedido { get; set; }
    }

    public class ManterPedidoViewModel
    {
        [Display(Name = "Pedido")]
        public decimal Pedido { get; set; }
        [Display(Name = "Cliente")]
        public string Cliente { get; set; }
        [Display(Name = "Representante")]
        public string Representante { get; set; }
        public bool PodeEditarPedido { get; set; }        
        public bool PodeCancelarItens { get; set; }
        public decimal Status { get; set; }
    }

    public class ConclusaoPedidoViewModel
    {
        public int ID { get; set; }
        public bool Editando { get; set; }

        public ConclusaoPedidoViewModel()
        {
            IDTipoPedido = -1;
            using (DalutexContext ctxDalutex = new DalutexContext())
            {
                CanailVenda = ctxDalutex.CANAIS_VENDA.Find(1);//MERCADO INTERNO
                ViaTransporte = ctxDalutex.COML_VIASTRANSPORTE.Find(1);//RODOVIARIA
                Frete = ctxDalutex.COML_TIPOSFRETE.Find(2);//FOB;
                Moeda = ctxDalutex.CADASTRO_MOEDAS.Find((int)Enums.Moedas.Real);                              
            }

            PorcentagemComissao = 4; //ALTERAR CONFORME O TIPO DE COLEÇÂO
        }

        #region Combos

        [Display(Name = "Condição de pagto")]
        public VW_CONDICAO_PGTO CondicaoPagto { get; set; }

        [Display(Name = "Moeda")]
        public CADASTRO_MOEDAS Moeda { get; set; }

        [Display(Name = "Via transporte")]
        public COML_VIASTRANSPORTE ViaTransporte { get; set; }
       
        [Display(Name = "Frete")]
        public COML_TIPOSFRETE Frete { get; set; }

        [Display(Name = "Local de vendas")]
        public List<LOCALVENDA> LocaisVenda { get; set; }
        [Display(Name = "Local de vendas")]
        public int? IDLocaisVenda { get; set; }

        [Display(Name = "Canal de vendas")]
        public CANAIS_VENDA CanailVenda { get; set; }

        [Display(Name = "Gerente de vendas")]
        public List<COML_GERENCIAS> GerentesVenda { get; set; }
        [Display(Name = "Gerente de vendas")]
        public int IDGerentesVenda { get; set; }

        [Display(Name = "Tipo de atendimento")]
        public PRE_PEDIDO_ATEND TipoAtendimento { get; set; }

        [Display(Name = "Qualidade comercial")]
        public KeyValuePair<string,string> QualidadeComercial { get; set; }

        [StringLength(1000)]
        [Display(Name="Observações")]
        public string Observacoes { get; set; }

        [Display(Name = "Pedido do Cliente")]
        public int? PedidoCliente { get; set; }

        [Display(Name = "Tipo de Pedido")]
        public string DescTipoPedido { get; set; }

        #endregion

        #region ValoresSelecionados

        public int IDTipoPedido { get; set; }
        [Display(Name="Representante")]
        public REPRESENTANTES Representante { get; set; }
        [Display(Name = "Cliente Fatura")]
        public VW_CLIENTE_TRANSP ClienteFatura { get; set; }
        [Display(Name = "Cliente Entrega")]
        public VW_CLIENTE_TRANSP ClienteEntrega { get; set; }
        [Display(Name = "Transportadora")]
        public TRANSPORTADORAS Transportadora { get; set; }
        public decimal PorcentagemComissao { get; set; }
        public DateTime DataEntrega { get; set; }
        public decimal TotalPedido { get; set; }
        public int StatusPedido { get; set; }
        
        #endregion

        #region Itens do carrinho

        public List<InserirNoCarrinhoViewModel> Itens { get; set; }

        #endregion        
    }    
}