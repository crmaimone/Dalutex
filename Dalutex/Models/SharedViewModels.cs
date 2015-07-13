using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Dalutex.Models.DataModels;
using System.Drawing;

namespace Dalutex.Models
{
    public class ThumbViewModel
    {
        public Enums.ItemType Tipo { get; set; }
        public string UrlImagens { get; set; }
        public string Desenho { get; set; }
        public string Variante { get; set; }
        public string Cor { get; set; }
        public string RGB { get; set; }
        public string Artigo { get; set; }
        public int Reduzido { get; set; }
        public int IDColecao { get; set; }
        public string NMColecao { get; set; }
        public int Pagina { get; set; }
        public string CodStudio { get; set; }
        public string CodDal { get; set; }
        public int IDStudio { get; set; }
        public int IDItemStudio { get; set; }
        public int PedidoReserva { get; set; }
        public int ItemPedidoReserva { get; set; }
        public int IDVariante { get; set; }
        public string Studio { get; set; }

        //Rotina PE..
        public string Tecnologia { get; set; }
        public string Composicao { get; set; }
        public decimal KGPrimeira { get; set; }
        public decimal KGSegunda { get; set; }
        public decimal KGTerceira { get; set; }
        public decimal MTPrimeira { get; set; }
        public decimal MTSegunda { get; set; }
        public decimal MTTerceira { get; set; }        
    }

    public class BuscaRepresentanteViewModel
    {
        [Required]
        [Display(Name = "Representante")]
        public int IDRepresentante { get; set; }
        public List<KeyValuePair<int,string>> Representantes { get; set; }

        public string Nome { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}