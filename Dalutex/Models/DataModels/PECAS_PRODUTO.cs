namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DALUTEX.PECAS_PRODUTO")]
    public partial class PECAS_PRODUTO
    {
        [Key]
        [Column(Order = 0)]
        public byte CODIGO_REGISTRO { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(7)]
        public string CODIGO_CLIENTE { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NUMERO_PECA { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short SEQUENCIA { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int REDUZIDO_PRODUTO_CRU { get; set; }

        public int? NUMERO_ROMANEIO_ENTR { get; set; }

        public int? DATA_DO_ROMANEIO_ENT { get; set; }

        public int? NUMERO_ROMANEIO_SAID { get; set; }

        public int? DATA_DO_ROMANEIO_SAI { get; set; }

        public int? NOTA_FISCAL_ENTRADA { get; set; }

        public int? NOTA_FISCAL_SAIDA { get; set; }

        public int? NUMERO_OB { get; set; }

        [StringLength(7)]
        public string CLIENTE_COMPROU_PECA { get; set; }

        public int? CODIGO_REDUZIDO_PROD { get; set; }

        public int? NUMERO_ETIQUETA_AUXI { get; set; }

        public byte? DIGITO_VERIFICADOR_E { get; set; }

        [StringLength(10)]
        public string NUMERO_BOX { get; set; }

        public byte? STATUS { get; set; }

        [StringLength(5)]
        public string PADRAO_QUALIDADE_ANA { get; set; }

        public byte? PADRAO_QUALIDADE_SIN { get; set; }

        public int? DATA_DA_ENTRADA_PECA { get; set; }

        public byte? TIPO_ACONDICIONAMENT { get; set; }

        [Column(TypeName = "float")]
        public decimal? PESO_PECA { get; set; }

        [Column(TypeName = "float")]
        public decimal? METROS_PECA { get; set; }

        public byte? FILIAL_ORIGEM_CRU { get; set; }

        public byte? FILIAL_ORIGEM_ACABAD { get; set; }

        [StringLength(15)]
        public string CODIGO_REVISORA { get; set; }

        [StringLength(10)]
        public string LOTE_PRODUTO { get; set; }

        [StringLength(5)]
        public string NUMEROTEAR { get; set; }

        public int? NUMERO_PECA_ORIGEM { get; set; }

        public short? SEQUENCIA_ORIGEM { get; set; }

        [Column(TypeName = "float")]
        public decimal? METROS_EMENDA { get; set; }

        [Column(TypeName = "float")]
        public decimal? LARGURA_INICIAL { get; set; }

        [Column(TypeName = "float")]
        public decimal? LARGURA_PECA { get; set; }

        [StringLength(3)]
        public string NUANCE { get; set; }

        [StringLength(2)]
        public string CODIGO_CLASSIFICACAO { get; set; }

        public int? CODIGO_OBSER { get; set; }

        public short? NUMERO_PONTOS { get; set; }

        public byte? CLASSIFICACAO_COR { get; set; }

        [StringLength(1)]
        public string COM_OPTICO { get; set; }

        [StringLength(10)]
        public string CODIGO_DA_GOMA { get; set; }

        public int? NUMERO_PILHA_ROLADA { get; set; }

        [StringLength(15)]
        public string OPERADOR0 { get; set; }

        [StringLength(15)]
        public string OPERADOR1 { get; set; }

        [StringLength(15)]
        public string OPERADOR2 { get; set; }

        [StringLength(15)]
        public string OPERADOR3 { get; set; }

        [StringLength(15)]
        public string OPERADOR4 { get; set; }

        [StringLength(1)]
        public string PECA_DE_RETALHO { get; set; }

        public byte? SEQUENCIA_DA_OB { get; set; }

        [StringLength(8)]
        public string BOX_INVENTARIO { get; set; }

        [StringLength(1)]
        public string PECA_DEVOLVIDA { get; set; }

        public short? CODIGO_FACCAO { get; set; }

        public int? NUMERO_PEDIDO_COMERC { get; set; }

        public short? SEQUENCIA_PRODUTO_PE { get; set; }

        [StringLength(10)]
        public string USUARIO_BAIXOU_PECA { get; set; }

        [StringLength(1)]
        public string PECA_DIVIDIDA { get; set; }

        public int? TEMPO_REVISAO { get; set; }

        [StringLength(1)]
        public string PECA_DE_REPROCESSO { get; set; }

        [StringLength(2)]
        public string GRUPO_DEFEITO { get; set; }

        public byte? TIPO_DEFEITO { get; set; }

        public byte? TOTAL_EMENDAS { get; set; }

        public byte? EMBALAGEM { get; set; }

        public short? QUANTIDADE_EMBALAGEN { get; set; }

        public int? NUMERO_VOLTAS_PECA { get; set; }

        [Column(TypeName = "float")]
        public decimal? GRAMATURA_PECA { get; set; }

        public short? CODIGO_DEPOSITO { get; set; }

        public int? DATA_ENTREGA_PECA { get; set; }

        public byte? TURNO_OPERADOR { get; set; }

        public int? NUMERO_ORDEM_PRODUCA { get; set; }

        public int? CODIGO_REGISTRO_ORIG { get; set; }

        public int? PRODUTO_DUPLICADO_OR { get; set; }

        public short? FINALIDADE_NO_BENEFI { get; set; }

        public byte? PECA_EM_GRUPO { get; set; }

        public int? NUMERO_MOVIMENTO_MVT { get; set; }

        public int? NUMERO_MOVIMENTO_M_1 { get; set; }

        public int? HORA_DA_ENTRADA_PECA { get; set; }

        public int? MVTO_EST_BAIXA_FIBRA { get; set; }

        public int? MVTO_EST_BAIXA_FIB_1 { get; set; }

        [StringLength(3)]
        public string SERIE_NOTA_ENTRADA { get; set; }

        [Column(TypeName = "float")]
        public decimal? PESO_BRUTO { get; set; }

        [Column(TypeName = "float")]
        public decimal? PESO_PRIMEIRA_PESAGE { get; set; }

        [StringLength(1)]
        public string CODREGCLF { get; set; }

        [StringLength(7)]
        public string CODCLF { get; set; }

        public short? SEQUENCIA_PECA_NA_OB { get; set; }

        public short? NRO_DE_PECAS { get; set; }

        [Column(TypeName = "float")]
        public decimal? SATURACAO_DAS_FIBRAS { get; set; }

        [Column(TypeName = "float")]
        public decimal? METROS_A_DIVIDIR { get; set; }

        public short? SEQUENCIA_AUXILIAR { get; set; }

        public int? NUMERO_FARDO { get; set; }

        [StringLength(1)]
        public string OURELA_CENTRO_OURELA { get; set; }

        public int? DATA_REVISAO { get; set; }

        public byte? STATUS_IMPORTACAO { get; set; }

        public byte? DESTINO_PECA { get; set; }

        [StringLength(1)]
        public string LADO_PECA { get; set; }

        [StringLength(1)]
        public string PECA_DE_EXPORTACAO { get; set; }

        public int? NUMERO_FER { get; set; }

        public int? NUMERO_PECA_DESTINO { get; set; }

        public short? SEQ_PECA_DESTINO { get; set; }

        public byte? STATUSPECAORIGEM { get; set; }

        public byte? TIPO_PECA { get; set; }

        public short? DESTINO_OB { get; set; }

        public int? FASE_REVISAO_OB { get; set; }

        [StringLength(8)]
        public string CARACTERISTICAS { get; set; }

        [StringLength(30)]
        public string ESCOLHEDEIRA { get; set; }

        public int? NRO_ETIQUETA_ARRIADA { get; set; }

        [Column(TypeName = "float")]
        public decimal? TARA_ADICIONAL { get; set; }

        [StringLength(1)]
        public string RESERVADA { get; set; }

        public int? DT_FINAL_RESERVA { get; set; }
    }
}
