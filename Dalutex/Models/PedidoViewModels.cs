using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dalutex.Models.DataModels;
using System.ComponentModel.DataAnnotations;

namespace Dalutex.Models
{
    public class TumbViewModel
    {
        public string UrlImagens { get; set; }
        public string Desenho { get; set; }
        public string Variante { get; set; }
        public int Reduzido { get; set; }
    }

    public class PedidoViewModel
    {
        public List<VW_COLECAO_ATUAL> Galeria { get; set; }
        public string UrlImagens { get; set; }
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
        public string Tecnologia { get; set; }
        public string UnidadeMedida { get; set; }
        public decimal ValorPadrao { get; set; }

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
        #region Combos

        [Display(Name="Tipo de pedido")]
        public List<COML_TIPOSPEDIDOS> TiposPedido { get; set; }

        [Display(Name = "Condição de pagto")]
        public List<VW_CONDICAO_PGTO> CondicoesPagto { get; set; }

        [Display(Name = "Moeda")]
        public List<CADASTRO_MOEDAS> Moedas { get; set; }

        [Display(Name = "Via transporte")]
        public List<COML_VIASTRANSPORTE> ViasTransporte { get; set; }

        [Display(Name = "Frete")]
        public List<COML_TIPOSFRETE> Fretes { get; set; }

        [Display(Name = "Local de vendas")]
        public List<LOCALVENDA> LocaisVenda { get; set; }

        [Display(Name = "Canal de vendas")]
        public List<CANAIS_VENDA> CanaisVenda { get; set; }

        [Display(Name = "Gerente de vendas")]
        public List<COML_GERENCIAS> GerentesVenda { get; set; }

        [Display(Name = "Tipo de atendimento")]
        public List<PRE_PEDIDO_ATEND> TiposAtendimento { get; set; }

        #endregion

        #region 

        #endregion

        #region Itens do carrinho

        public List<InserirNoCarrinhoViewModel> Itens { get; set; }

        #endregion
    }
}