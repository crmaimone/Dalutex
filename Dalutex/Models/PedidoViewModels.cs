using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dalutex.Models.DataModels;
using System.ComponentModel.DataAnnotations;

namespace Dalutex.Models
{
    public class DesenhoVariante
    {
        public string Desenho { get; set; }
        public string Variante { get; set; }
    }

    public class DesenhosPorColecaoViewModel
    {
        public int IDColecao { get; set; }
        public int Pagina { get; set; }
        public int TotalPaginas { get; set; }
        public List<DesenhoVariante> Galeria { get; set; }
        public string UrlImagens { get; set; }
    }

    public class MenuColecoesViewModel
    {
        public string Filtro { get; set; }
        public List<VW_COLECAO> Colecoes { get; set; }
    }

    public class ArtigosDisponiveisViewModel
    {
        public string Desenho { get; set; }
        public string Variante { get; set; }
        public string Imagem { get; set; }
        public List<VW_ARTIGOS_DISPONIVEIS> Artigos { get; set; }
    }

    public class InserirNoCarrinhoViewModel
    {
        public string Desenho { get; set; }
        public string Variante { get; set; }
        public string Artigo { get; set; }
        public string TecnologiaPorExtenso { get; set; }
        public string Tecnologia { get { return this.TecnologiaPorExtenso != null ? this.TecnologiaPorExtenso.Substring(0, 1) : null; } }
        public string UnidadeMedida { get; set; }
        public decimal ValorPadrao { get; set; }
        public bool ObterTipoPedido { get; set; }
        public int Reduzido { get; set; }
        public DateTime DataEntregaItem { get; set; }

        public int Comissao {get; set; } //ver com cassiano onde criar este field e como mapear a regra para comissão (vide coleção);

        [Required]
        [Display(Name = "Tipo de pedido")]
        public int IDTipoPedido { get; set; }
        public List<COML_TIPOSPEDIDOS> TiposPedido { get; set; }

        [Required]
        [Display(Name="Peças")]
        public int Pecas { get; set; }

        [DataType(DataType.Currency)]
        public decimal Quantidade { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }
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
        }

        #region Combos

        [Display(Name = "Condição de pagto")]
        public List<VW_CONDICAO_PGTO> CondicoesPagto { get; set; }
        public int IDCondicoesPagto { get; set; }

        [Display(Name = "Moeda")]
        public List<CADASTRO_MOEDAS> Moedas { get; set; }
        public int IDMoedas { get; set; }

        [Display(Name = "Via transporte")]
        public List<COML_VIASTRANSPORTE> ViasTransporte { get; set; }
        public int IDViasTransporte { get; set; }

        [Display(Name = "Frete")]
        public List<COML_TIPOSFRETE> Fretes { get; set; }
        public int IDFretes { get; set; }

        [Display(Name = "Local de vendas")]
        public List<LOCALVENDA> LocaisVenda { get; set; }
        public int IDLocaisVenda { get; set; }

        [Display(Name = "Canal de vendas")]
        public List<CANAIS_VENDA> CanaisVenda { get; set; }
        public int IDCanaisVenda { get; set; }

        [Display(Name = "Gerente de vendas")]
        public List<COML_GERENCIAS> GerentesVenda { get; set; }
        public int IDGerentesVenda { get; set; }

        [Display(Name = "Tipo de atendimento")]
        public List<PRE_PEDIDO_ATEND> TiposAtendimento { get; set; }
        public int IDTiposAtendimento { get; set; }

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
        public int IDClienteEntrega { get; set; }
        public int IDTransportadora { get; set; }
        public decimal PorcentagemComissao { get; set; }
        public DateTime DataEntrega { get; set; }

        #endregion

        #region Itens do carrinho

        public List<InserirNoCarrinhoViewModel> Itens { get; set; }

        #endregion
    }
}