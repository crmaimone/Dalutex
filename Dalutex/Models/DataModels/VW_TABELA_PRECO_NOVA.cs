namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.VW_TABELA_PRECO_NOVA")]
    public partial class VW_TABELA_PRECO_NOVA
    {
        [Key]
        [Column(Order = 0)]
        public decimal ID_TABELA_PRECO_ITEM { get; set; }
        public decimal? PRECO { get; set; }
        
        [StringLength(10)]
        public string ARTIGO { get; set; }
        
        public bool LISTA_ATIVA { get; set; }
        public int? CONDICAO_PAGAMENTO { get; set; }
        public int ID_TABELA_PRECO { get; set; }
        public int TIPO { get; set; }
        
        [StringLength(10)]
        public string TECNOLOGIA { get; set; }
        public int? QUALIDADE { get; set; }
        
        [StringLength(1)]
        public string QUALIDADE_COMERCIAL { get; set; }
        
        [StringLength(1)]
        public string TAMANHO_PECA { get; set; }
        public decimal? COMISSAO { get; set; }
        
        [StringLength(10)]
        public string CLASSIFICACAO { get; set; }
    }
}
