using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dalutex.Models.DataModels;
using System.ComponentModel.DataAnnotations;

namespace Dalutex.Models
{
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

        [Required]
        [Display(Name="Peças")]
        public int Pecas { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Quilos")]
        public decimal Quilos { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }
    }
}