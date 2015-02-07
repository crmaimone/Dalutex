namespace Dalutex.Models.DataModels
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DalutexContext : DbContext
    {
        public DalutexContext()
            : base("name=DalutexContext")
        {
        }

        public virtual DbSet<COLECOES> COLECOES { get; set; }
        public virtual DbSet<PECAS_PRODUTO> PECAS_PRODUTO { get; set; }
        public virtual DbSet<ITENS_ESTOQUE> ITENS_ESTOQUE { get; set; }
        public virtual DbSet<COML_GERENCIAS> COML_GERENCIAS { get; set; }
        public virtual DbSet<COML_TIPOSFRETE> COML_TIPOSFRETE { get; set; }
        public virtual DbSet<COML_VIASTRANSPORTE> COML_VIASTRANSPORTE { get; set; }
        public virtual DbSet<CANAIS_VENDA> CANAIS_VENDA { get; set; }
        public virtual DbSet<COML_CONDICOESPAGAME> COML_CONDICOESPAGAME { get; set; }
        public virtual DbSet<CADASTRO_MOEDAS> CADASTRO_MOEDAS { get; set; }
        public virtual DbSet<COML_TIPOSPEDIDOS> COML_TIPOSPEDIDOS { get; set; }
        public virtual DbSet<REPRESENTANTES> REPRESENTANTES { get; set; }
        public virtual DbSet<COML_CONTATOS> COML_CONTATOS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<REPRESENTANTES>()
                .Property(e => e.NOME)
                .IsUnicode(false);

            modelBuilder.Entity<REPRESENTANTES>()
                .Property(e => e.COMISSAONAAREA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REPRESENTANTES>()
                .Property(e => e.COMISSAOFORAAREA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REPRESENTANTES>()
                .Property(e => e.PERCCOMISSAOFATURA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REPRESENTANTES>()
                .Property(e => e.PERCCOMISSAOBAIXA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<COML_TIPOSPEDIDOS>()
                .Property(e => e.DESCRICAO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<COML_TIPOSPEDIDOS>()
                .Property(e => e.STEXIGEREPRESENTANTE)
                .IsUnicode(false);

            modelBuilder.Entity<COLECOES>()
                .Property(e => e.ID_COLECAO)
                .IsUnicode(false);

            modelBuilder.Entity<COLECOES>()
                .Property(e => e.NOME)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.CODIGO_CLIENTE)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.CLIENTE_COMPROU_PECA)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.NUMERO_BOX)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.PADRAO_QUALIDADE_ANA)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.PESO_PECA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.METROS_PECA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.CODIGO_REVISORA)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.LOTE_PRODUTO)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.NUMEROTEAR)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.METROS_EMENDA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.LARGURA_INICIAL)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.LARGURA_PECA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.NUANCE)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.CODIGO_CLASSIFICACAO)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.COM_OPTICO)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.CODIGO_DA_GOMA)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.OPERADOR0)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.OPERADOR1)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.OPERADOR2)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.OPERADOR3)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.OPERADOR4)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.PECA_DE_RETALHO)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.BOX_INVENTARIO)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.PECA_DEVOLVIDA)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.USUARIO_BAIXOU_PECA)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.PECA_DIVIDIDA)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.PECA_DE_REPROCESSO)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.GRUPO_DEFEITO)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.GRAMATURA_PECA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.SERIE_NOTA_ENTRADA)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.PESO_BRUTO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.PESO_PRIMEIRA_PESAGE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.CODREGCLF)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.CODCLF)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.SATURACAO_DAS_FIBRAS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.METROS_A_DIVIDIR)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.OURELA_CENTRO_OURELA)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.LADO_PECA)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.PECA_DE_EXPORTACAO)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.CARACTERISTICAS)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.ESCOLHEDEIRA)
                .IsUnicode(false);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.TARA_ADICIONAL)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PECAS_PRODUTO>()
                .Property(e => e.RESERVADA)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.CODIGO)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.CODIGO_COMERCIAL)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.CODIGO_ALTERNATIVO)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.DESCRICAO)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.NOME_DETALHADO1)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.NOME_DETALHADO2)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.CODCLASFISCAL)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.UNIMED)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.ESTMIN_NAOUSAR)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.ESTOQUE_MAXIMONAOUSA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.PRCINS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.PRCATU)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.PRCMED)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.PERCENTUAL_IPI)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.FORNECEDOR_ULTIMA_CO)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.CODIGO_TRIBUTACAO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.GRUPO_ESTOQUE)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.QTDADE_ULTIMA_COMPRA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.INSUMO_COM_MOVIMENTO)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.DEBITO_DIRETO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.UNIMEDALTERNATIVO)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.TESTE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.FATORCONVERSAOMEDIDA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.CONC_SOLUCAO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.TEMTESTECONCENTRACAO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.PRECO_CUSTO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.PRECO_VENDA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.PRECO_TRANSFERENCIA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.EXPORTAR_ESTOQUE)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.FATORCONVERSAOPESO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.CONTA_CONTABIL)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.GERA_ESTOQUE)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.ENTRADA_COM_PEDIDO)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.PRECO_PEDIDO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.CLASSE_GERENCIAL)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.NOME_QUEM_CADASTROU)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.NOME_QUEM_ALTEROU)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.CODIGO_PROGRAMACAO)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.WKPRCINS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.WKPRCATU)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.WKPRCMED)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.ENTRADAULTTRESMESES0)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.ENTRADAULTTRESMESES1)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.ENTRADAULTTRESMESES2)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.SAIDAULTTRESMESES0)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.SAIDAULTTRESMESES1)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.SAIDAULTTRESMESES2)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.CODIGO_TITULO)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.GERA_PRECO_MEDIO)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.BLOQUEADO)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.GRUPO_LUCRO_BRUTO)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.FATOR_SATURACAO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.CODIGO_FAMILIA)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.LOTE_ECONOMICO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.FAMILIA_ESTOQUE)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.PROJETO)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.ACRESC_SUBS_TRIBUTAR)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.PERC_REDUCAO_ICMS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.USA_RESERVA_PECAS)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.CODREG_CLF_EXCLUSIVO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.COD_GRUPO_FIO_TECIDO)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.EH_PNEUMAFIL)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.EH_RESIDUO_PAVIO)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.CADASTRADO)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.IDENTIFICACAO)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.FATORCONVERSAOLITRO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.GERADOPELOSISTEMA)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.CODIGO_FAMILIA_POV)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.GERAOPSUSPANTESCORTE)
                .IsUnicode(false);

            modelBuilder.Entity<ITENS_ESTOQUE>()
                .Property(e => e.EMPRESA_ITENS)
                .IsUnicode(false);

            modelBuilder.Entity<COML_CONTATOS>()
                .Property(e => e.CONTATO)
                .IsUnicode(false);

            modelBuilder.Entity<COML_CONTATOS>()
                .Property(e => e.CARGO)
                .IsUnicode(false);

            modelBuilder.Entity<COML_CONTATOS>()
                .Property(e => e.TELEFONE)
                .IsUnicode(false);

            modelBuilder.Entity<COML_CONTATOS>()
                .Property(e => e.RAMAL)
                .IsUnicode(false);

            modelBuilder.Entity<COML_CONTATOS>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<COML_CONTATOS>()
                .Property(e => e.CELULAR)
                .IsUnicode(false);
        }
    }
}
