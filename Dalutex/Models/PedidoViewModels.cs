using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dalutex.Models.DataModels;
using System.ComponentModel.DataAnnotations;
using Dalutex.Models.Utils;
using System.Drawing;

namespace Dalutex.Models
{
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
    }

    public class ArtigoTecnologia
    {
        public string Artigo { get; set; }
        public string Tecnologia { get; set; }
    }

    public class Liso
    {
        public string Artigo { get; set; }
        public string Cor { get; set; }
        public string RGB { get; set; }
        public int Reduzido { get; set; }
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
    }


    public class ItensProntaEntregaViewModel
    {
        public int Reduzido { get; set; }
        public int Pagina { get; set; }
        public int TotalPaginas { get; set; }
        public List<VW_ITENS_PE> ListaDesenhosPE { get; set; }
        public List<VW_ITENS_PE> ListaLisosPE { get; set; }
        public string UrlImagens { get; set; }
        public string FiltroDesenho { get; set; }
        public Enums.ItemType Tipo { get; set; }
    }

    public class LisosViewModel
    {
        public int IDColecao { get; set; }
        public string NMColecao { get; set; }
        public int Pagina { get; set; }
        public int TotalPaginas { get; set; }
        public List<Liso> Galeria { get; set; }
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
    }

    public class InserirNoCarrinhoViewModel
    {
        public InserirNoCarrinhoViewModel()
        {
            this.ComposeOpcoes = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };//Valores fixos de zero a dez. verificar necessidade de mais.
            this.Compose = 0;
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
        public bool ObterTipoPedido { get; set; }
        public int Reduzido { get; set; }

        public DateTime DataEntregaItem { get; set; }
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

        [Required]
        [Display(Name = "TIPO DE PEDIDO:")]
        public int IDTipoPedido { get; set; }
        public List<COML_TIPOSPEDIDOS> TiposPedido { get; set; }

        [Required]
        [Display(Name="PEÇAS:")]
        public int Pecas { get; set; }

        [DataType(DataType.Currency)]
        public decimal Quantidade { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "PREÇO:")]
        public decimal Preco { get; set; }

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
        public Enums.ItemType Tipo { get; set; }
        public string CodStudio { get; set; }
        public string RGB { get; set; }
    }

    public class ConclusaoPedidoViewModel
    {
        public ConclusaoPedidoViewModel()
        {
            IDTipoPedido = -1;
            IDCanaisVenda = 1; //MERCADO INTERNO
            IDViasTransporte = 1; //RODOVIARIA
            IDFretes = 2; //FOB
            PorcentagemComissao = 4; //ALTERAR CONFORME O TIPO DE COLEÇÂO
            IDMoedas = (int)Enums.Moedas.Real;
        }

        #region Combos

        [Display(Name = "Condição de pagto")]
        public List<VW_CONDICAO_PGTO> CondicoesPagto { get; set; }
        [Display(Name = "Condição de pagto")]
        public int? IDCondicoesPagto { get; set; }

        [Display(Name = "Moeda")]
        public List<CADASTRO_MOEDAS> Moedas { get; set; }
        [Display(Name = "Moeda")]
        public int? IDMoedas { get; set; }

        [Display(Name = "Via transporte")]
        public List<COML_VIASTRANSPORTE> ViasTransporte { get; set; }
        [Display(Name = "Via transporte")]
        public int? IDViasTransporte { get; set; }

        [Display(Name = "Frete")]
        public List<COML_TIPOSFRETE> Fretes { get; set; }
        [Display(Name = "Frete")]
        public int? IDFretes { get; set; }

        [Display(Name = "Local de vendas")]
        public List<LOCALVENDA> LocaisVenda { get; set; }
        [Display(Name = "Local de vendas")]
        public int? IDLocaisVenda { get; set; }

        [Display(Name = "Canal de vendas")]
        public List<CANAIS_VENDA> CanaisVenda { get; set; }
        [Display(Name = "Canal de vendas")]
        public int? IDCanaisVenda { get; set; }

        [Display(Name = "Gerente de vendas")]
        public List<COML_GERENCIAS> GerentesVenda { get; set; }
        [Display(Name = "Gerente de vendas")]
        public int IDGerentesVenda { get; set; }

        [Display(Name = "Tipo de atendimento")]
        public List<PRE_PEDIDO_ATEND> TiposAtendimento { get; set; }
        public int? IDTiposAtendimento { get; set; }

        [Display(Name = "Qualidade comercial")]
        public List<KeyValuePair<string, string>> QualidadeComercial { get; set; }
        public string IDQualidadeComercial { get; set; }

        [StringLength(1000)]
        [Display(Name="Observações")]
        public string Observacoes { get; set; }

        #endregion

        #region ValoresSelecionados

        public int IDTipoPedido { get; set; }
        public int IDRepresentante { get; set; }
        public int IDClienteFatura { get; set; }
        public int? IDClienteEntrega { get; set; }
        public int? IDTransportadora { get; set; }
        public decimal PorcentagemComissao { get; set; }
        public DateTime DataEntrega { get; set; }
        public decimal TotalPedido { get; set; }

        #endregion

        #region Itens do carrinho

        public List<InserirNoCarrinhoViewModel> Itens { get; set; }

        #endregion
    }
}