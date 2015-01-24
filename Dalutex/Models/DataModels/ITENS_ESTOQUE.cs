namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DALUTEX.ITENS_ESTOQUE")]
    public partial class ITENS_ESTOQUE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CODIGO_REDUZIDO { get; set; }

        [StringLength(30)]
        public string CODIGO { get; set; }

        public byte? CODIGO_REGISTRO { get; set; }

        [StringLength(30)]
        public string CODIGO_COMERCIAL { get; set; }

        [StringLength(30)]
        public string CODIGO_ALTERNATIVO { get; set; }

        [StringLength(60)]
        public string DESCRICAO { get; set; }

        [StringLength(60)]
        public string NOME_DETALHADO1 { get; set; }

        [StringLength(60)]
        public string NOME_DETALHADO2 { get; set; }

        [StringLength(10)]
        public string CODCLASFISCAL { get; set; }

        [StringLength(2)]
        public string UNIMED { get; set; }

        [Column(TypeName = "float")]
        public decimal? ESTMIN_NAOUSAR { get; set; }

        [Column(TypeName = "float")]
        public decimal? ESTOQUE_MAXIMONAOUSA { get; set; }

        [Column(TypeName = "float")]
        public decimal? PRCINS { get; set; }

        [Column(TypeName = "float")]
        public decimal? PRCATU { get; set; }

        [Column(TypeName = "float")]
        public decimal? PRCMED { get; set; }

        public int? DATA_ULTIMA_COMPRA { get; set; }

        public int? DATA_ULTIMO_MOVIMENT { get; set; }

        public int? DATA_ULTIMA_ALTERACA { get; set; }

        [Column(TypeName = "float")]
        public decimal? PERCENTUAL_IPI { get; set; }

        [StringLength(7)]
        public string FORNECEDOR_ULTIMA_CO { get; set; }

        public byte? STATUS_COMPRA { get; set; }

        [StringLength(1)]
        public string CODIGO_TRIBUTACAO { get; set; }

        [StringLength(10)]
        public string GRUPO_ESTOQUE { get; set; }

        [Column(TypeName = "float")]
        public decimal? QTDADE_ULTIMA_COMPRA { get; set; }

        [StringLength(1)]
        public string INSUMO_COM_MOVIMENTO { get; set; }

        [StringLength(1)]
        public string DEBITO_DIRETO { get; set; }

        [StringLength(2)]
        public string UNIMEDALTERNATIVO { get; set; }

        [StringLength(1)]
        public string TESTE { get; set; }

        [Column(TypeName = "float")]
        public decimal? FATORCONVERSAOMEDIDA { get; set; }

        [Column(TypeName = "float")]
        public decimal? CONC_SOLUCAO { get; set; }

        [StringLength(1)]
        public string TEMTESTECONCENTRACAO { get; set; }

        public short? CONCENTRACAOINSUMOS { get; set; }

        public short? CONCENTRACAOPADRAO { get; set; }

        [Column(TypeName = "float")]
        public decimal? PRECO_CUSTO { get; set; }

        [Column(TypeName = "float")]
        public decimal? PRECO_VENDA { get; set; }

        [Column(TypeName = "float")]
        public decimal? PRECO_TRANSFERENCIA { get; set; }

        [StringLength(1)]
        public string EXPORTAR_ESTOQUE { get; set; }

        public int? ESPECIFICACAOPRODUTO { get; set; }

        [Column(TypeName = "float")]
        public decimal? FATORCONVERSAOPESO { get; set; }

        [StringLength(20)]
        public string CONTA_CONTABIL { get; set; }

        [StringLength(1)]
        public string GERA_ESTOQUE { get; set; }

        [StringLength(1)]
        public string ENTRADA_COM_PEDIDO { get; set; }

        [Column(TypeName = "float")]
        public decimal? PRECO_PEDIDO { get; set; }

        [StringLength(15)]
        public string CLASSE_GERENCIAL { get; set; }

        public short? LINHA_PRODUTO { get; set; }

        public byte? COMPOSICAO_NOTA_FISC { get; set; }

        public int? DATA_CADASTRAMENTO { get; set; }

        public byte? FILIAL_ULT_ALTERACAO { get; set; }

        [StringLength(30)]
        public string NOME_QUEM_CADASTROU { get; set; }

        [StringLength(30)]
        public string NOME_QUEM_ALTEROU { get; set; }

        [StringLength(23)]
        public string CODIGO_PROGRAMACAO { get; set; }

        [Column(TypeName = "float")]
        public decimal? WKPRCINS { get; set; }

        [Column(TypeName = "float")]
        public decimal? WKPRCATU { get; set; }

        [Column(TypeName = "float")]
        public decimal? WKPRCMED { get; set; }

        public int? WKULTCOM { get; set; }

        public int? WKULTMOV { get; set; }

        public int? ULTIMAALTERACAOPRECO { get; set; }

        public byte? ULTIMOS_TRES_MESES0 { get; set; }

        public byte? ULTIMOS_TRES_MESES1 { get; set; }

        public byte? ULTIMOS_TRES_MESES2 { get; set; }

        [Column(TypeName = "float")]
        public decimal? ENTRADAULTTRESMESES0 { get; set; }

        [Column(TypeName = "float")]
        public decimal? ENTRADAULTTRESMESES1 { get; set; }

        [Column(TypeName = "float")]
        public decimal? ENTRADAULTTRESMESES2 { get; set; }

        [Column(TypeName = "float")]
        public decimal? SAIDAULTTRESMESES0 { get; set; }

        [Column(TypeName = "float")]
        public decimal? SAIDAULTTRESMESES1 { get; set; }

        [Column(TypeName = "float")]
        public decimal? SAIDAULTTRESMESES2 { get; set; }

        [StringLength(10)]
        public string CODIGO_TITULO { get; set; }

        [StringLength(1)]
        public string GERA_PRECO_MEDIO { get; set; }

        [StringLength(1)]
        public string BLOQUEADO { get; set; }

        [StringLength(10)]
        public string GRUPO_LUCRO_BRUTO { get; set; }

        [Column(TypeName = "float")]
        public decimal? FATOR_SATURACAO { get; set; }

        public int? GRUPO_USTER { get; set; }

        [StringLength(20)]
        public string CODIGO_FAMILIA { get; set; }

        public int? PRAZOMEDIOENTREGA { get; set; }

        public byte? TIPOREPOSICAO { get; set; }

        [Column(TypeName = "float")]
        public decimal? LOTE_ECONOMICO { get; set; }

        public byte? FILIAL_IDEAL_PRODUC { get; set; }

        [StringLength(20)]
        public string FAMILIA_ESTOQUE { get; set; }

        public short? GRUPOCONVERSAO { get; set; }

        [StringLength(8)]
        public string PROJETO { get; set; }

        public byte? STATUS_ATIVACAO { get; set; }

        [Column(TypeName = "float")]
        public decimal? ACRESC_SUBS_TRIBUTAR { get; set; }

        [Column(TypeName = "float")]
        public decimal? PERC_REDUCAO_ICMS { get; set; }

        public short? DIAS_ITEM_NO_ESTOQUE { get; set; }

        public int? LINHAPRODUTOCOMERCIA { get; set; }

        public int? COLECAO { get; set; }

        [StringLength(1)]
        public string USA_RESERVA_PECAS { get; set; }

        public byte? STATUS_IMPORTACAO { get; set; }

        [StringLength(1)]
        public string CODREG_CLF_EXCLUSIVO { get; set; }

        public int? REDUZIDO_BORDADO { get; set; }

        [StringLength(10)]
        public string COD_GRUPO_FIO_TECIDO { get; set; }

        [StringLength(1)]
        public string EH_PNEUMAFIL { get; set; }

        [StringLength(1)]
        public string EH_RESIDUO_PAVIO { get; set; }

        public int? COD_SEGMENTO_MERCADO { get; set; }

        public byte? COD_ACONDICIONAMENTO { get; set; }

        public byte? COD_ACOND_SECUNDARIO { get; set; }

        public byte? COD_EMBALAGEM_PADRAO { get; set; }

        public int? ESTRUTURA { get; set; }

        [StringLength(1)]
        public string CADASTRADO { get; set; }

        public int? IDGRUPOREFERENCIA { get; set; }

        [StringLength(30)]
        public string IDENTIFICACAO { get; set; }

        public int? TIPOITEM { get; set; }

        public int? SITUACAO { get; set; }

        public int? IDCOMPOSICAO { get; set; }

        public int? FILIALULTIMAALTERACA { get; set; }

        public int? IDTIPOREPOSICAO { get; set; }

        public int? GRUPOLUCROBRUTO { get; set; }

        public byte? TIPO_ITEM { get; set; }

        public int? ATUALIZADOEM { get; set; }

        public int? IDGRUPOPROGRAMACAO { get; set; }

        [Column(TypeName = "float")]
        public decimal? FATORCONVERSAOLITRO { get; set; }

        [StringLength(1)]
        public string GERADOPELOSISTEMA { get; set; }

        [StringLength(20)]
        public string CODIGO_FAMILIA_POV { get; set; }

        [StringLength(1)]
        public string GERAOPSUSPANTESCORTE { get; set; }

        public byte? STATUS_ENGENHARIA { get; set; }

        public int? REDUZIDOFIGURA { get; set; }

        public int? CODIGOOBSERVACAO { get; set; }

        public int? ITEMCOMPRADO { get; set; }

        [StringLength(10)]
        public string EMPRESA_ITENS { get; set; }

        public int? IDSITUTRIBUIPI { get; set; }
    }
}
