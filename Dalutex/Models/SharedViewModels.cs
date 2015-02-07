using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Dalutex.Models.DataModels;

namespace Dalutex.Models
{
    public class ThumbViewModel
    {
        public string UrlImagens { get; set; }
        public string Desenho { get; set; }
        public string Variante { get; set; }
        public int Reduzido { get; set; }
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