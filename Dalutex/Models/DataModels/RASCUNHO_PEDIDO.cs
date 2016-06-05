namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.RASCUNHO_PEDIDO")]
    public partial class RASCUNHO_PEDIDO
    {
        [Key]
        public decimal PEDIDO { get; set; }

        public DateTime? DATA_EMISSAO { get; set; }

        public decimal? ID_REPRESENTANTE { get; set; }

        public decimal? ID_CLIENTE { get; set; }

        public decimal? STATUS_PEDIDO { get; set; }

        [StringLength(1)]
        public string CLIENTE_NOVO { get; set; }

        [StringLength(2)]
        public string ESTADO { get; set; }

        public decimal? TIPO_PEDIDO { get; set; }

        [StringLength(30)]
        public string USUARIO_STATUS { get; set; }

        public DateTime? DATA_ALT_STATUS { get; set; }

        [StringLength(1)]
        public string QUALIDADE_COM { get; set; }

        public decimal? COD_COND_PGTO { get; set; }

        [StringLength(1000)]
        public string OBSERVACOES { get; set; }

        [StringLength(60)]
        public string NOME_CLIENTE { get; set; }

        public DateTime? DATA_EMISSAO_DT { get; set; }

        public DateTime? DATA_ENTREGA { get; set; }

        public DateTime? DATA_ENTREGA_DIGI { get; set; }

        [StringLength(20)]
        public string ORIGEM { get; set; }

        public decimal? ID_CLIENTE_ENTREGA { get; set; }

        public decimal? ID_TRANSPORTADORA { get; set; }

        [StringLength(30)]
        public string USUARIO_INICIO { get; set; }

        public DateTime? DATA_INICIO { get; set; }

        [StringLength(30)]
        public string USUARIO_FINAL { get; set; }

        public DateTime? DATA_FINAL { get; set; }

        [StringLength(20)]
        public string CNPJ { get; set; }

        [StringLength(1)]
        public string FLAG_DATA_OK_APS { get; set; }

        public decimal? ID_LOCAL { get; set; }

        public decimal? ID_STATUS { get; set; }

        [StringLength(20)]
        public string CNPJ_ENTREGA { get; set; }

        public decimal? COD_MOEDA { get; set; }

        public short? CANAL_VENDAS { get; set; }

        public decimal? GERENTE { get; set; }

        public decimal? ATENDIMENTO { get; set; }

        public decimal? VIATRANSPORTE { get; set; }

        public decimal? TIPOFRETE { get; set; }

        public decimal? NUMERO_CARTAO { get; set; }

        public DateTime? VALIDADE { get; set; }

        [StringLength(30)]
        public string BANDEIRA { get; set; }

        [StringLength(30)]
        public string BANCO { get; set; }

        [StringLength(200)]
        public string MOTIVO_CANC { get; set; }

        public decimal? COMISSAO { get; set; }

        public decimal? ID_CLI_FACCAO { get; set; }

        [StringLength(100)]
        public string GRUPO_FACCAO { get; set; }

        public decimal? PEDIDO_CLIENTE { get; set; }
    }
}
