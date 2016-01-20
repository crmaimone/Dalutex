namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.VW_IMPRESSAO_WEB")]
    public partial class VW_IMPRESSAO_WEB
    {
        public decimal PEDIDO { get; set; }

        [Key]
        [Column(Order = 0)]
        public decimal PEDIDO_BLOCO { get; set; }

        public DateTime? DATA_EMISSAO { get; set; }

        public decimal? STATUS_PEDIDO { get; set; }

        [StringLength(1)]
        public string CLIENTE_NOVO { get; set; }

        [StringLength(2)]
        public string ESTADO { get; set; }

        [StringLength(1)]
        public string QUALIDADE_COM { get; set; }

        [StringLength(1000)]
        public string OBSERVACOES { get; set; }

        [StringLength(60)]
        public string NOME_CLIENTE { get; set; }

        [StringLength(40)]
        public string DESCRI_COND { get; set; }

        [StringLength(1)]
        public string LISO_ESTAMP { get; set; }

        [StringLength(1)]
        public string MALHA_PLANO { get; set; }

        [StringLength(1)]
        public string MODA_DECORACAO { get; set; }

        [StringLength(4)]
        public string ARTIGO { get; set; }

        [StringLength(7)]
        public string COR { get; set; }

        [StringLength(4)]
        public string DESENHO { get; set; }

        [StringLength(2)]
        public string VARIANTE { get; set; }

        public decimal? QUANTIDADE { get; set; }

        public decimal? PRECO_UNIT { get; set; }

        public decimal? VALOR_TOTAL { get; set; }

        public decimal? TOTAL_MT { get; set; }
        public decimal? TOTAL_KG { get; set; }

        [StringLength(2)]
        public string UM { get; set; }

        [StringLength(1)]
        public string PE { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal ITEM { get; set; }

        public decimal? ID_CLIENTE { get; set; }

        public int? COD_CLI { get; set; }

        [StringLength(60)]
        public string NOME_CLIENTE_PJ { get; set; }

        [StringLength(60)]
        public string CLIENTE_CAD_OU_NOVO { get; set; }

        [StringLength(2)]
        public string ESTADO_CLI { get; set; }

        [StringLength(20)]
        public string CNPJ { get; set; }

        [StringLength(25)]
        public string TIPO_PEDIDO { get; set; }

        public int? COD_REP { get; set; }

        [StringLength(30)]
        public string NOME_REPRES { get; set; }

        [StringLength(32)]
        public string COLECAO { get; set; }

        public DateTime? DT_ENT_DISP_PED { get; set; }

        public DateTime? DT_ENT_DIG_PED { get; set; }

        public DateTime? DT_ENT_DISP_IT { get; set; }

        public DateTime? DT_ENT_DIG_IT { get; set; }

        public decimal? ID_CLI_ENTR { get; set; }

        [StringLength(60)]
        public string CLIE_ENTR { get; set; }

        [StringLength(20)]
        public string CNPJ_ENTR { get; set; }

        public decimal? ID_TRANSP { get; set; }

        [StringLength(25)]
        public string TRANSPORTADORA { get; set; }

        [StringLength(30)]
        public string USUARIO_INICIO { get; set; }

        public DateTime? DATA_INICIO { get; set; }

        public DateTime? DATA_FINAL { get; set; }

        public decimal? COMPOSE { get; set; }

        [StringLength(4000)]
        public string IMAGEM { get; set; }

        [Column(TypeName = "float")]
        public decimal? COMISSAO { get; set; }

        public decimal? REDUZIDO_ITEM { get; set; }

        [StringLength(25)]
        public string VIA_TRANSP { get; set; }

        [StringLength(40)]
        public string CANAL_VENDAS { get; set; }

        [StringLength(25)]
        public string FRETE { get; set; }

        [StringLength(20)]
        public string GERENTE { get; set; }

        [StringLength(100)]
        public string DESCRI_ATEND { get; set; }

        public decimal? PRECOLISTA { get; set; }

        [StringLength(4)]
        public string TIPO { get; set; }

        public decimal? TIPO_PED { get; set; }

        [StringLength(140)]
        public string RESERVA { get; set; }

        [StringLength(60)]
        public string OBS_TROCA_TEC { get; set; }

        //PEDIDO_CLIENTE
        public decimal? PEDIDO_CLIENTE { get; set; }

        public decimal? IDTRANSPORTADORA { get; set; }

        public string GRUPO_CLIENTE { get; set; }

        public int COD_CLI_ENTR { get; set; }
    }
}
