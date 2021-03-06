namespace Dalutex.Models.DataModels
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TIDalutexContext : DbContext
    {
        public TIDalutexContext()
            : base("name=TIDalutexContext")
        {
        }

        public virtual DbSet<CONTROLE_DESENV> CONTROLE_DESENV { get; set; }
        public virtual DbSet<CONTROLE_DESENV_CARAC_TEC> CONTROLE_DESENV_CARAC_TEC { get; set; }
        public virtual DbSet<CONTROLE_DESENV_ITEM_STUDIO> CONTROLE_DESENV_ITEM_STUDIO { get; set; }
        public virtual DbSet<CONTROLE_DESENV_TECNOLOGIA> CONTROLE_DESENV_TECNOLOGIA { get; set; }
        public virtual DbSet<PED_ARTIGO_ATIVO> PED_ARTIGO_ATIVO { get; set; }
        public virtual DbSet<PED_ARTIGO_PROCESSO> PED_ARTIGO_PROCESSO { get; set; }
        public virtual DbSet<PED_TECNOLOGIA> PED_TECNOLOGIA { get; set; }
        public virtual DbSet<PRE_PEDIDO> PRE_PEDIDO { get; set; }
        public virtual DbSet<USUARIOS> USUARIOS { get; set; }
        public virtual DbSet<PRE_PEDIDO_ITENS> PRE_PEDIDO_ITENS { get; set; }
        public virtual DbSet<PED_LINK_RESTRICAO_ARTIGO> PED_LINK_RESTRICAO_ARTIGO { get; set; }
        public virtual DbSet<CONTROLE_DESENV_LINK_CT> CONTROLE_DESENV_LINK_CT { get; set; }
        public virtual DbSet<CONFIG_GERAL> CONFIG_GERAL { get; set; }
        public virtual DbSet<VW_CARACT_DESENHOS> VW_CARACT_DESENHOS { get; set; }
        public virtual DbSet<VW_DESENHOS_POR_COLECAO> VW_DESENHOS_POR_COLECAO { get; set; }
        public virtual DbSet<VW_ARTIGOS_DISPONIVEIS> VW_ARTIGOS_DISPONIVEIS { get; set; }
        public virtual DbSet<LOCALVENDA> LOCALVENDA { get; set; }
        public virtual DbSet<PRE_PEDIDO_ATEND> PRE_PEDIDO_ATEND { get; set; }
        public virtual DbSet<PRE_PEDIDO_COND_PAG> PRE_PEDIDO_COND_PAG { get; set; }
        public virtual DbSet<TAMANHOPECA> TAMANHOPECA { get; set; }
        public virtual DbSet<VW_CONDICAO_PGTO> VW_CONDICAO_PGTO { get; set; }
        public virtual DbSet<ARTIGO_PESO_PADRAO> ARTIGO_PESO_PADRAO { get; set; }
        public virtual DbSet<VW_CLIENTE_TRANSP> VW_CLIENTE_TRANSP { get; set; }
        public virtual DbSet<PROXIMO_NUMERO_PEDIDO> PROXIMO_NUMERO_PEDIDO { get; set; }
        public virtual DbSet<CRIACAO_REDUZIDOS> CRIACAO_REDUZIDOS { get; set; }
        public virtual DbSet<DISPONIBILIDADE_MALHA> DISPONIBILIDADE_MALHA { get; set; }
        public virtual DbSet<VW_COLECAO> VW_COLECAO { get; set; }
        public virtual DbSet<PRE_PEDIDO_CRITICA> PRE_PEDIDO_CRITICA { get; set; }
        public virtual DbSet<VW_TROCA_TEC> VW_TROCA_TEC { get; set; }
        public virtual DbSet<VW_LISOS_POR_COLECAO> VW_LISOS_POR_COLECAO { get; set; }
        public virtual DbSet<VW_IMPRESSAO_WEB> VW_IMPRESSAO_WEB { get; set; }
        public virtual DbSet<VW_DESENHOS_DISP_RESERVA> VW_DESENHOS_DISP_RESERVA { get; set; }
        public virtual DbSet<VW_ITENS_VALIDAR_RESERVA> VW_ITENS_VALIDAR_RESERVA { get; set; }
        public virtual DbSet<VW_VALIDAR_RESERVA> VW_VALIDAR_RESERVA { get; set; }
        public virtual DbSet<PED_RESERVA_VENDA> PED_RESERVA_VENDA { get; set; }
        public virtual DbSet<PED_LINK_QUANTD_TIPO> PED_LINK_QUANTD_TIPO { get; set; }
        public virtual DbSet<VW_ITENS_PE> VW_ITENS_PE { get; set; }
        public virtual DbSet<VW_LISTA_PECAS_PE> VW_LISTA_PECAS_PE { get; set; }
        public virtual DbSet<VW_FAROL> VW_FAROL { get; set; }   
        public virtual DbSet<VW_DESENHOS_POR_COL_DESENV> VW_DESENHOS_POR_COL_DESENV { get; set; }
        public virtual DbSet<REGRA_PADRAO> REGRA_PADRAO { get; set; }
        public virtual DbSet<VW_PI_RED> VW_PI_RED { get; set; }
        public virtual DbSet<REGRAS_QTD_PEDIDO> REGRAS_QTD_PEDIDO { get; set; }
        public virtual DbSet<VW_PESQUISA_PEDIDO> VW_PESQUISA_PEDIDO { get; set; }
        public virtual DbSet<VW_EMAILS> VW_EMAILS { get; set; }
        public virtual DbSet<VW_ORDEM_ANTERIOR_BLOQ> VW_ORDEM_ANTERIOR_BLOQ { get; set; }
        public virtual DbSet<VW_POCKET_ATUAL> VW_POCKET_ATUAL { get; set; }
        public virtual DbSet<VW_PEDIDO_ROM_FAT> VW_PEDIDO_ROM_FAT { get; set; }
        public virtual DbSet<ACOES> ACOES { get; set; }
        public virtual DbSet<USUARIOS_ACOES> USUARIOS_ACOES { get; set; }
        public virtual DbSet<LOG_PEDIDO_WEB> LOG_PEDIDO_WEB { get; set; }
        public virtual DbSet<VW_ORDEM_ANTERIOR_BLOQ_NEW> VW_ORDEM_ANTERIOR_BLOQ_NEW { get; set; }
        public virtual DbSet<VW_TABELA_PRECO_NOVA> VW_TABELA_PRECO_NOVA { get; set; }
        public virtual DbSet<VW_GRUPO_COL_RED> VW_GRUPO_COL_RED { get; set; }
        public virtual DbSet<REGRAS_QTD_PEDIDOX> REGRAS_QTD_PEDIDOX { get; set; }
        public virtual DbSet<VW_ARTIGOS_DISPONIVEIS_NEW> VW_ARTIGOS_DISPONIVEIS_NEW { get; set; }
        public virtual DbSet<VW_CARACT_DESENHOS_NEW> VW_CARACT_DESENHOS_NEW { get; set; }
        public virtual DbSet<RASCUNHO_PEDIDO> RASCUNHO_PEDIDO { get; set; }
        public virtual DbSet<RASCUNHO_PEDIDO_ITEM> RASCUNHO_PEDIDO_ITEM { get; set; }
        public virtual DbSet<VW_RASCUNHO_PEDIDOS> VW_RASCUNHO_PEDIDOS { get; set; }
        public virtual DbSet<VW_ARTIGOS_INATIVOS> VW_ARTIGOS_INATIVOS { get; set; }
        public virtual DbSet<LINK_GRUPO_COND_PGTO> LINK_GRUPO_COND_PGTO { get; set; }
        public virtual DbSet<VW_CUS_CONS_TAB_PRECO> VW_CUS_CONS_TAB_PRECO { get; set; }
        public virtual DbSet<TIPO_PEDIDO_USUARIO> TIPO_PEDIDO_USUARIO { get; set; }
        public virtual DbSet<VW_CLIE_OPER_TRINGULAR> VW_CLIE_OPER_TRINGULAR { get; set; }
        public virtual DbSet<VW_NF_PEDIDO_CLIENTE> VW_NF_PEDIDO_CLIENTE { get; set; }
        public virtual DbSet<EMAIL_TABELA_PRECO> EMAIL_TABELA_PRECO { get; set; }
        public virtual DbSet<VW_EMAIL_USUARIO> VW_EMAIL_USUARIO { get; set; }
        public virtual DbSet<CONTROLE_DESENV_COLECAO> CONTROLE_DESENV_COLECAO { get; set; }
        public virtual DbSet<VW_VERIFICA_SE_VAR_EXCL> VW_VERIFICA_SE_VAR_EXCL { get; set; }
        public virtual DbSet<GRUPO_CLIENTE_SGT> GRUPO_CLIENTE_SGT { get; set; }
        public virtual DbSet<VW_ACORDO_DISPO_CLIENTE> VW_ACORDO_DISPO_CLIENTE { get; set; }
        public virtual DbSet<ACORDO_COMERCIAL_PEDIDOS> ACORDO_COMERCIAL_PEDIDOS { get; set; }
        public virtual DbSet<VW_ACORDOS_VIGENTES> VW_ACORDOS_VIGENTES { get; set; }
        public virtual DbSet<VW_DES_TEM_ATEND_ABERTO> VW_DES_TEM_ATEND_ABERTO { get; set; }
        public virtual DbSet<TECNOLOGIAS> TECNOLOGIAS { get; set; }
        public virtual DbSet<VW_GRUPO_CLIENTE_OU_CLIENTE> VW_GRUPO_CLIENTE_OU_CLIENTE { get; set; }
        public virtual DbSet<ACORDO_COMERCIAL_ITENS> ACORDO_COMERCIAL_ITENS { get; set; }
        public virtual DbSet<VW_TOTAL_VEND_DESENHO> VW_TOTAL_VEND_DESENHO { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ACORDO_COMERCIAL_ITENS>()
               .Property(e => e.ID_ITEM_ACORDO_COM)
               .HasPrecision(38, 0);

            modelBuilder.Entity<ACORDO_COMERCIAL_ITENS>()
                .Property(e => e.ID_ACORDO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ACORDO_COMERCIAL_ITENS>()
                .Property(e => e.ARTIGO)
                .IsUnicode(false);

            modelBuilder.Entity<ACORDO_COMERCIAL_ITENS>()
                .Property(e => e.DESENHO)
                .IsUnicode(false);

            modelBuilder.Entity<ACORDO_COMERCIAL_ITENS>()
                .Property(e => e.VARIANTE)
                .IsUnicode(false);

            modelBuilder.Entity<ACORDO_COMERCIAL_ITENS>()
                .Property(e => e.COR)
                .IsUnicode(false);

            modelBuilder.Entity<ACORDO_COMERCIAL_ITENS>()
                .Property(e => e.QUANTIDADES)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ACORDO_COMERCIAL_ITENS>()
                .Property(e => e.UM)
                .IsUnicode(false);

            modelBuilder.Entity<ACORDO_COMERCIAL_ITENS>()
                .Property(e => e.PRECO_UNITARIO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ACORDO_COMERCIAL_ITENS>()
                .Property(e => e.QTDE_DISPONIVEL)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ACORDO_COMERCIAL_ITENS>()
                .Property(e => e.TECNOLOGIA)
                .IsUnicode(false);

            modelBuilder.Entity<ACORDO_COMERCIAL_ITENS>()
                .Property(e => e.QTDE_DISPONIVEL_TMP)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ACORDO_COMERCIAL_ITENS>()
                .Property(e => e.CONDICAO_PGTO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ACORDO_COMERCIAL_ITENS>()
                .Property(e => e.QUALIDADE_COM)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ACORDO_COMERCIAL_ITENS>()
                .Property(e => e.COMISSAO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ACORDO_COMERCIAL_ITENS>()
                .Property(e => e.ID_GRUPO_COND_PGTO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ACORDO_COMERCIAL_ITENS>()
                .Property(e => e.MARGEM)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_GRUPO_CLIENTE_OU_CLIENTE>()
                .Property(e => e.ID_CLIENTE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_GRUPO_CLIENTE_OU_CLIENTE>()
                .Property(e => e.ID_GRUPO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_GRUPO_CLIENTE_OU_CLIENTE>()
                .Property(e => e.GRUPO)
                .IsUnicode(false);

            modelBuilder.Entity<TECNOLOGIAS>()
                .Property(e => e.ID_TEC)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TECNOLOGIAS>()
                .Property(e => e.DESC_TEC)
                .IsUnicode(false);

            modelBuilder.Entity<TECNOLOGIAS>()
                .Property(e => e.COD_ID)
                .IsUnicode(false);

            modelBuilder.Entity<TECNOLOGIAS>()
                .Property(e => e.DESC_OUTRO_IDIOMA)
                .IsUnicode(false);


            modelBuilder.Entity<VW_DES_TEM_ATEND_ABERTO>()
                .Property(e => e.ID_CONTROLE_DESENV)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_DES_TEM_ATEND_ABERTO>()
                .Property(e => e.DESENHO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_ACORDOS_VIGENTES>()
               .Property(e => e.ID_REG)
               .HasPrecision(38, 0);

            modelBuilder.Entity<VW_ACORDOS_VIGENTES>()
                .Property(e => e.ID_ACORDO_COMERCIAL)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_ACORDOS_VIGENTES>()
                .Property(e => e.CLIENTE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_ACORDOS_VIGENTES>()
                .Property(e => e.NOME_CLIENTE)
                .IsUnicode(false);

            modelBuilder.Entity<VW_ACORDOS_VIGENTES>()
                .Property(e => e.GRUPO_CLIENTE)
                .IsUnicode(false);

            modelBuilder.Entity<VW_ACORDOS_VIGENTES>()
                .Property(e => e.ID_REP)
                .IsUnicode(false);

            modelBuilder.Entity<VW_ACORDOS_VIGENTES>()
                .Property(e => e.REPRESENTANTE)
                .IsUnicode(false);

            modelBuilder.Entity<VW_ACORDOS_VIGENTES>()
                .Property(e => e.USUARIO_DIGITADOR)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_ACORDOS_VIGENTES>()
                .Property(e => e.USUARIO_APROVACAO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_ACORDOS_VIGENTES>()
                .Property(e => e.OBSERVACAO_APROVACAO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_ACORDOS_VIGENTES>()
                .Property(e => e.OBSERVACAO_DIGITACAO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_ACORDOS_VIGENTES>()
                .Property(e => e.ID_ITEM_ACORDO_COM)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_ACORDOS_VIGENTES>()
                .Property(e => e.ID_ACORDO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_ACORDOS_VIGENTES>()
                .Property(e => e.ARTIGO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_ACORDOS_VIGENTES>()
                .Property(e => e.DESENHO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_ACORDOS_VIGENTES>()
                .Property(e => e.VARIANTE)
                .IsUnicode(false);

            modelBuilder.Entity<VW_ACORDOS_VIGENTES>()
                .Property(e => e.COR)
                .IsUnicode(false);

            modelBuilder.Entity<VW_ACORDOS_VIGENTES>()
                .Property(e => e.QUANTIDADES)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_ACORDOS_VIGENTES>()
                .Property(e => e.QTDE_DISPONIVEL)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_ACORDOS_VIGENTES>()
                .Property(e => e.UM)
                .IsUnicode(false);

            modelBuilder.Entity<VW_ACORDOS_VIGENTES>()
                .Property(e => e.PRECO_UNITARIO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_ACORDOS_VIGENTES>()
                .Property(e => e.USUARIO_DIG)
                .IsUnicode(false);

            modelBuilder.Entity<VW_ACORDOS_VIGENTES>()
                .Property(e => e.USUARIO_APROV)
                .IsUnicode(false);

            modelBuilder.Entity<ACORDO_COMERCIAL_PEDIDOS>()
                .Property(e => e.ID_ACORDO_COM_PED)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ACORDO_COMERCIAL_PEDIDOS>()
                .Property(e => e.ID_ACORDO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ACORDO_COMERCIAL_PEDIDOS>()
                .Property(e => e.PEDIDO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ACORDO_COMERCIAL_PEDIDOS>()
                .Property(e => e.ITEM)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ACORDO_COMERCIAL_PEDIDOS>()
                .Property(e => e.QUANTIDADE)
                .HasPrecision(38, 0);


            modelBuilder.Entity<VW_ACORDO_DISPO_CLIENTE>()
                .Property(e => e.ID_ITEM_ACORDO_COM)
                .HasPrecision(38, 0);

            //modelBuilder.Entity<VW_ACORDO_DISPO_CLIENTE>()
            //    .Property(e => e.CLIENTE)
            //    .HasPrecision(38, 0);

            modelBuilder.Entity<VW_ACORDO_DISPO_CLIENTE>()
                .Property(e => e.ARTIGO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_ACORDO_DISPO_CLIENTE>()
                .Property(e => e.TECNOLOGIA)
                .IsUnicode(false);

            modelBuilder.Entity<VW_ACORDO_DISPO_CLIENTE>()
                .Property(e => e.QTDE_DISPONIVEL)
                .HasPrecision(38, 0);
            
            modelBuilder.Entity<VW_ACORDO_DISPO_CLIENTE>()
                .Property(e => e.QTDE_DISPONIVEL_TMP)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_ACORDO_DISPO_CLIENTE>()
                .Property(e => e.PRECO_UNITARIO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_ACORDO_DISPO_CLIENTE>()
                .Property(e => e.CONDICAO_PGTO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_ACORDO_DISPO_CLIENTE>()
                .Property(e => e.COMISSAO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_ACORDO_DISPO_CLIENTE>()
                .Property(e => e.ID_GRUPO_COND_PGTO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<GRUPO_CLIENTE_SGT>()
                .Property(e => e.ID_GRUPO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<GRUPO_CLIENTE_SGT>()
                .Property(e => e.ID_CLIENTE_SGT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<GRUPO_CLIENTE_SGT>()
                .Property(e => e.PK_GRUPO_CLIENTE_SGT)
                .HasPrecision(38, 0);


            modelBuilder.Entity<VW_VERIFICA_SE_VAR_EXCL>()
                .Property(e => e.ID_VAR_EXC)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_VERIFICA_SE_VAR_EXCL>()
                .Property(e => e.DESENHO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_VERIFICA_SE_VAR_EXCL>()
                .Property(e => e.VARIANTE)
                .IsUnicode(false);

            modelBuilder.Entity<VW_VERIFICA_SE_VAR_EXCL>()
                .Property(e => e.ID_GRUPO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_VERIFICA_SE_VAR_EXCL>()
                .Property(e => e.ID_CLIENTE_SGT)
                .HasPrecision(38, 0);


            modelBuilder.Entity<CONTROLE_DESENV_COLECAO>()
                .Property(e => e.REDUZIDO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV_COLECAO>()
                .Property(e => e.COLECAO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV_COLECAO>()
                .Property(e => e.USUARIO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV_COLECAO>()
                .Property(e => e.PEDIDO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV_COLECAO>()
                .Property(e => e.OCORRENCIA)
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV_COLECAO>()
                .Property(e => e.ARTIGO)
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV_COLECAO>()
                .Property(e => e.DESENHO)
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV_COLECAO>()
                .Property(e => e.VARIANTE)
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV_COLECAO>()
                .Property(e => e.TECNOLOGIA)
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV_COLECAO>()
                .Property(e => e.COR)
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV_COLECAO>()
                .Property(e => e.ID_CONTROLE_DESENV_COLECAO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV_COLECAO>()
                .Property(e => e.ID_USUARIO_OCORR)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV_COLECAO>()
                .Property(e => e.ARTIGO_OCORRENCIA)
                .IsUnicode(false);


            modelBuilder.Entity<VW_EMAIL_USUARIO>()
                .Property(e => e.ID_USUARIO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_EMAIL_USUARIO>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<EMAIL_TABELA_PRECO>()
                .Property(e => e.ID_EMAIL_TABELA_PRECO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<EMAIL_TABELA_PRECO>()
                .Property(e => e.ID_UAUARIO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<EMAIL_TABELA_PRECO>()
                .Property(e => e.STATUS_ENVIO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_NF_PEDIDO_CLIENTE>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_NF_PEDIDO_CLIENTE>()
                .Property(e => e.ID_CLIENTE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_CLIE_OPER_TRINGULAR>()
                .Property(e => e.ID_)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_CLIE_OPER_TRINGULAR>()
                .Property(e => e.COD_CLIENTE)
                .IsUnicode(false);

            modelBuilder.Entity<VW_CUS_CONS_TAB_PRECO>()
                .Property(e => e.ID_TABELA_PRECO_ITEM)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_CUS_CONS_TAB_PRECO>()
                .Property(e => e.NOME_USU)
                .IsUnicode(false);

            modelBuilder.Entity<VW_CUS_CONS_TAB_PRECO>()
                .Property(e => e.ID_TABELA_PRECO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_CUS_CONS_TAB_PRECO>()
                .Property(e => e.ARTIGO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_CUS_CONS_TAB_PRECO>()
                .Property(e => e.TECNOLOGIA)
                .IsUnicode(false);

            modelBuilder.Entity<VW_CUS_CONS_TAB_PRECO>()
                .Property(e => e.CLASSIFICACAO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_CUS_CONS_TAB_PRECO>()
                .Property(e => e.CONDICAO_PAGAMENTO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_CUS_CONS_TAB_PRECO>()
                .Property(e => e.QUALIDADE_COMERCIAL)
                .IsUnicode(false);

            //modelBuilder.Entity<VW_CUS_CONS_TAB_PRECO>()
            //    .Property(e => e.COMISSAO)
            //    .HasPrecision(38, 0);

            modelBuilder.Entity<VW_CUS_CONS_TAB_PRECO>()
                .Property(e => e.PRECO)
                .HasPrecision(38, 0);

            //modelBuilder.Entity<VW_CUS_CONS_TAB_PRECO>()
            //    .Property(e => e.RENDIMENTO)
            //    .HasPrecision(38, 0);

            //modelBuilder.Entity<VW_CUS_CONS_TAB_PRECO>()
            //    .Property(e => e.LARGURA)
            //    .HasPrecision(38, 0);

            //modelBuilder.Entity<VW_CUS_CONS_TAB_PRECO>()
            //    .Property(e => e.GRAMATURA)
            //    .HasPrecision(38, 0);

            modelBuilder.Entity<VW_CUS_CONS_TAB_PRECO>()
                .Property(e => e.COMPOSICAO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_RASCUNHO_PEDIDOS>()
                .Property(e => e.PEDIDO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_RASCUNHO_PEDIDOS>()
                .Property(e => e.CLIENTE)
                .IsUnicode(false);

            modelBuilder.Entity<VW_RASCUNHO_PEDIDOS>()
                .Property(e => e.REPRESENTANTE)
                .IsUnicode(false);

            modelBuilder.Entity<VW_RASCUNHO_PEDIDOS>()
                .Property(e => e.STATUS_PEDIDO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
               .Property(e => e.PEDIDO)
               .HasPrecision(38, 0);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.ID_REPRESENTANTE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.ID_CLIENTE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.STATUS_PEDIDO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.CLIENTE_NOVO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.ESTADO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.TIPO_PEDIDO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.USUARIO_STATUS)
                .IsUnicode(false);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.QUALIDADE_COM)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.COD_COND_PGTO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.OBSERVACOES)
                .IsUnicode(false);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.NOME_CLIENTE)
                .IsUnicode(false);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.ORIGEM)
                .IsUnicode(false);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.ID_CLIENTE_ENTREGA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.ID_TRANSPORTADORA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.USUARIO_INICIO)
                .IsUnicode(false);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.USUARIO_FINAL)
                .IsUnicode(false);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.CNPJ)
                .IsUnicode(false);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.FLAG_DATA_OK_APS)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.ID_LOCAL)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.ID_STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.CNPJ_ENTREGA)
                .IsUnicode(false);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.GERENTE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.ATENDIMENTO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.NUMERO_CARTAO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.BANDEIRA)
                .IsUnicode(false);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.BANCO)
                .IsUnicode(false);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.MOTIVO_CANC)
                .IsUnicode(false);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.COMISSAO)
                .HasPrecision(4, 2);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.ID_CLI_FACCAO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.GRUPO_FACCAO)
                .IsUnicode(false);

            modelBuilder.Entity<RASCUNHO_PEDIDO>()
                .Property(e => e.PEDIDO_CLIENTE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RASCUNHO_PEDIDO_ITEM>()
                .Property(e => e.PEDIDO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RASCUNHO_PEDIDO_ITEM>()
                .Property(e => e.REDUZIDO_ITEM)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RASCUNHO_PEDIDO_ITEM>()
                .Property(e => e.STATUS_ITEM)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RASCUNHO_PEDIDO_ITEM>()
                .Property(e => e.LISO_ESTAMP)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<RASCUNHO_PEDIDO_ITEM>()
                .Property(e => e.MALHA_PLANO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<RASCUNHO_PEDIDO_ITEM>()
                .Property(e => e.MODA_DECORACAO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<RASCUNHO_PEDIDO_ITEM>()
                .Property(e => e.ARTIGO)
                .IsUnicode(false);

            modelBuilder.Entity<RASCUNHO_PEDIDO_ITEM>()
                .Property(e => e.COR)
                .IsUnicode(false);

            modelBuilder.Entity<RASCUNHO_PEDIDO_ITEM>()
                .Property(e => e.DESENHO)
                .IsUnicode(false);

            modelBuilder.Entity<RASCUNHO_PEDIDO_ITEM>()
                .Property(e => e.VARIANTE)
                .IsUnicode(false);

            modelBuilder.Entity<RASCUNHO_PEDIDO_ITEM>()
                .Property(e => e.QUANTIDADE)
                .HasPrecision(10, 2);

            modelBuilder.Entity<RASCUNHO_PEDIDO_ITEM>()
                .Property(e => e.PRECO_UNIT)
                .HasPrecision(10, 2);

            modelBuilder.Entity<RASCUNHO_PEDIDO_ITEM>()
                .Property(e => e.VALOR_TOTAL)
                .HasPrecision(10, 2);

            modelBuilder.Entity<RASCUNHO_PEDIDO_ITEM>()
                .Property(e => e.UM)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<RASCUNHO_PEDIDO_ITEM>()
                .Property(e => e.COLECAO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RASCUNHO_PEDIDO_ITEM>()
                .Property(e => e.PE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<RASCUNHO_PEDIDO_ITEM>()
                .Property(e => e.ITEM)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RASCUNHO_PEDIDO_ITEM>()
                .Property(e => e.SIT_ITEM)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RASCUNHO_PEDIDO_ITEM>()
                .Property(e => e.ORIGEM)
                .IsUnicode(false);

            modelBuilder.Entity<RASCUNHO_PEDIDO_ITEM>()
                .Property(e => e.COMPOSE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<RASCUNHO_PEDIDO_ITEM>()
                .Property(e => e.COD_COMPOSE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RASCUNHO_PEDIDO_ITEM>()
                .Property(e => e.ID_TAB_PRECO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RASCUNHO_PEDIDO_ITEM>()
                .Property(e => e.QUALIDADE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RASCUNHO_PEDIDO_ITEM>()
                .Property(e => e.PRECODIGITADOMOEDA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RASCUNHO_PEDIDO_ITEM>()
                .Property(e => e.PRECOLISTA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RASCUNHO_PEDIDO_ITEM>()
                .Property(e => e.QTDEPC)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RASCUNHO_PEDIDO_ITEM>()
                .Property(e => e.TROCA_TECNOLOGIA)
                .IsUnicode(false);

            modelBuilder.Entity<RASCUNHO_PEDIDO_ITEM>()
                .Property(e => e.RESTRICAO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_ARTIGOS_DISPONIVEIS_NEW>()
                .Property(e => e.ARTIGO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VW_ARTIGOS_DISPONIVEIS_NEW>()
                .Property(e => e.TECNOLOGIA)
                .IsUnicode(false);

            modelBuilder.Entity<VW_ARTIGOS_DISPONIVEIS_NEW>()
                .Property(e => e.ID_CARAC_TEC)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_ARTIGOS_DISPONIVEIS_NEW>()
                .Property(e => e.ID_TECNOLOGIA);

            modelBuilder.Entity<VW_ARTIGOS_DISPONIVEIS_NEW>()
                .Property(e => e.ART_DISP_PCP)
                .IsUnicode(false);

            modelBuilder.Entity<VW_ARTIGOS_DISPONIVEIS_NEW>()
                .Property(e => e.REST_DES)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_GRUPO_COL_RED>()
                .Property(e => e.TIPO_COL)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_TABELA_PRECO_NOVA>()
               .Property(e => e.ID_TABELA_PRECO_ITEM)
               .HasPrecision(38, 0);

            modelBuilder.Entity<VW_TABELA_PRECO_NOVA>()
                .Property(e => e.PRECO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_TABELA_PRECO_NOVA>()
                .Property(e => e.ARTIGO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_TABELA_PRECO_NOVA>()
                .Property(e => e.TECNOLOGIA)
                .IsUnicode(false);

            modelBuilder.Entity<VW_TABELA_PRECO_NOVA>()
                .Property(e => e.QUALIDADE_COMERCIAL)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VW_TABELA_PRECO_NOVA>()
                .Property(e => e.TAMANHO_PECA)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VW_TABELA_PRECO_NOVA>()
                .Property(e => e.COMISSAO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOG_PEDIDO_WEB>()
                .Property(e => e.ID_LOG_PEDIDO_WEB)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOG_PEDIDO_WEB>()
                .Property(e => e.DS_LOG_PEDIDO_WEB)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_PEDIDO_WEB>()
                .Property(e => e.USU_LOG_PEDIDO_WEB)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_PEDIDO_WEB>()
                .Property(e => e.TP_LOG_PEDIDO_WEB)
                .IsUnicode(false);

            modelBuilder.Entity<REGRAS_QTD_PEDIDO>()
                .Property(e => e.ID_REGRAS_QTD_PEDIDO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REGRAS_QTD_PEDIDO>()
                .Property(e => e.TECNOLOGIA)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<REGRAS_QTD_PEDIDO>()
                .Property(e => e.PROCESSO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REGRAS_QTD_PEDIDO>()
                .Property(e => e.TECNOLOGIA_DESTINO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<REGRAS_QTD_PEDIDO>()
                .Property(e => e.PROCESSO_DESTINO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REGRAS_QTD_PEDIDO>()
                .Property(e => e.GRUPO_COLECAO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REGRAS_QTD_PEDIDO>()
                .Property(e => e.TIPO_PEDIDO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REGRAS_QTD_PEDIDO>()
                .Property(e => e.UM)
                .IsUnicode(false);

            modelBuilder.Entity<REGRAS_QTD_PEDIDO>()
                .Property(e => e.QTD_MIN_VAR)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REGRAS_QTD_PEDIDO>()
                .Property(e => e.QTD_MAX_VAR)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REGRAS_QTD_PEDIDO>()
                .Property(e => e.QTD_MIN_DES)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REGRAS_QTD_PEDIDO>()
                .Property(e => e.QTD_MAX_DES)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REGRAS_QTD_PEDIDO>()
                .Property(e => e.USUARIO_EXCLUSAO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REGRA_PADRAO>()
                .Property(e => e.ID_REGRA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REGRA_PADRAO>()
                .Property(e => e.TIPO_PRODUTO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<REGRA_PADRAO>()
                .Property(e => e.TECNOLOGIA)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<REGRA_PADRAO>()
                .Property(e => e.ARTIGO)
                .IsUnicode(false);

            modelBuilder.Entity<REGRA_PADRAO>()
                .Property(e => e.ID_PROCESSO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REGRA_PADRAO>()
                .Property(e => e.REDUZIDO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REGRA_PADRAO>()
                .Property(e => e.VALOR_PADRAO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REGRA_PADRAO>()
                .Property(e => e.ID_USUARIO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REGRA_PADRAO>()
                .Property(e => e.UM)
                .IsUnicode(false);

            modelBuilder.Entity<REGRA_PADRAO>()
                .Property(e => e.ID_USUARIO_STATUS)
                .HasPrecision(38, 0);



            modelBuilder.Entity<VW_DESENHOS_POR_COL_DESENV>()
                .Property(e => e.COLECAO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_DESENHOS_POR_COL_DESENV>()
                .Property(e => e.DESENHO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_DESENHOS_POR_COL_DESENV>()
                .Property(e => e.VARIANTE)
                .IsUnicode(false);



            modelBuilder.Entity<VW_LISTA_PECAS_PE>()
               .Property(e => e.PESO_PECA)
               .HasPrecision(38, 0);

            modelBuilder.Entity<VW_LISTA_PECAS_PE>()
                .Property(e => e.METROS_PECA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PED_RESERVA_VENDA>()
                .Property(e => e.ID_PED_RESERVA_VENDA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_ITENS_VALIDAR_RESERVA>()
                .Property(e => e.ID_CONTROLE);

            modelBuilder.Entity<VW_ITENS_VALIDAR_RESERVA>()
                .Property(e => e.ID_REPRESENTANTE);

            modelBuilder.Entity<VW_ITENS_VALIDAR_RESERVA>()
                .Property(e => e.COD_STUDIO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_ITENS_VALIDAR_RESERVA>()
                .Property(e => e.DESENHO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_ITENS_VALIDAR_RESERVA>()
                .Property(e => e.COD_DAL)
                .IsUnicode(false);

            modelBuilder.Entity<VW_ITENS_VALIDAR_RESERVA>()
                .Property(e => e.VARIANTE)
                .IsUnicode(false);


            //TODO: ver com cassiano pq esta dando erro com ".HasPrecision(38, 0);" - alterei de decimal para int no datamodel
            modelBuilder.Entity<VW_ITENS_VALIDAR_RESERVA>()
                .Property(e => e.PEDIDO_RESERVA);
            //.HasPrecision(38, 0); 

            modelBuilder.Entity<VW_ITENS_VALIDAR_RESERVA>()
                .Property(e => e.ID_VAR);
            //.HasPrecision(38, 0);

            modelBuilder.Entity<VW_ITENS_VALIDAR_RESERVA>()
                .Property(e => e.ID_CLIENTE);
            //.HasPrecision(38, 0);.HasPrecision(38, 0);

            modelBuilder.Entity<VW_ITENS_VALIDAR_RESERVA>()
                .Property(e => e.IT_PEDIDO_RES);
            //.HasPrecision(38, 0);

            modelBuilder.Entity<VW_VALIDAR_RESERVA>()
                .Property(e => e.ID_REP)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_VALIDAR_RESERVA>()
                .Property(e => e.REPRESENTANTE)
                .IsUnicode(false);

            modelBuilder.Entity<VW_VALIDAR_RESERVA>()
                .Property(e => e.CLIENTE)
                .IsUnicode(false);

            modelBuilder.Entity<VW_VALIDAR_RESERVA>()
                .Property(e => e.PEDIDO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_VALIDAR_RESERVA>()
                .Property(e => e.ITEM_PEDIDO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_VALIDAR_RESERVA>()
                .Property(e => e.ID_CONTROLE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_VALIDAR_RESERVA>()
                .Property(e => e.COD_STUDIO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_VALIDAR_RESERVA>()
                .Property(e => e.STUDIO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_VALIDAR_RESERVA>()
                .Property(e => e.DESENHO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_VALIDAR_RESERVA>()
                .Property(e => e.COD_DAL)
                .IsUnicode(false);

            modelBuilder.Entity<VW_VALIDAR_RESERVA>()
                .Property(e => e.VARIANTE)
                .IsUnicode(false);

            modelBuilder.Entity<VW_VALIDAR_RESERVA>()
                .Property(e => e.DIGITADOR)
                .IsUnicode(false);

            modelBuilder.Entity<VW_DESENHOS_DISP_RESERVA>()
                .Property(e => e.ID_ITEM_STUDIO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_DESENHOS_DISP_RESERVA>()
                .Property(e => e.ID_CONTROLE_DESENV)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_DESENHOS_DISP_RESERVA>()
                .Property(e => e.ID_STUDIO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_DESENHOS_DISP_RESERVA>()
                .Property(e => e.COD_STUDIO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_DESENHOS_DISP_RESERVA>()
                .Property(e => e.NOME_STUDIO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_DESENHOS_DISP_RESERVA>()
                .Property(e => e.DESENHO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_DESENHOS_DISP_RESERVA>()
                .Property(e => e.COD_DAL)
                .IsUnicode(false);

            modelBuilder.Entity<VW_DESENHOS_DISP_RESERVA>()
                .Property(e => e.ID_CLIENTE)
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.TOTAL_MT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.TOTAL_KG)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.PEDIDO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.PEDIDO_BLOCO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.STATUS_PEDIDO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.CLIENTE_NOVO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.ESTADO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.QUALIDADE_COM)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.OBSERVACOES)
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.NOME_CLIENTE)
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.DESCRI_COND)
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.LISO_ESTAMP)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.MALHA_PLANO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.MODA_DECORACAO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.ARTIGO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.COR)
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.DESENHO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.VARIANTE)
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.QUANTIDADE)
                .HasPrecision(10, 2);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.PRECO_UNIT)
                .HasPrecision(10, 2);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.VALOR_TOTAL)
                .HasPrecision(10, 2);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.UM)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.PE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.ITEM)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.ID_CLIENTE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.NOME_CLIENTE_PJ)
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.CLIENTE_CAD_OU_NOVO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.ESTADO_CLI)
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.CNPJ)
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.TIPO_PEDIDO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.NOME_REPRES)
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.COLECAO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.ID_CLI_ENTR)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.CLIE_ENTR)
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.CNPJ_ENTR)
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.ID_TRANSP)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.TRANSPORTADORA)
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.USUARIO_INICIO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.COMPOSE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.IMAGEM)
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.COMISSAO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.REDUZIDO_ITEM)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.VIA_TRANSP)
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.CANAL_VENDAS)
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.FRETE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.GERENTE)
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.DESCRI_ATEND)
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.PRECOLISTA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.TIPO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.TIPO_PED)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.RESERVA)
                .IsUnicode(false);

            modelBuilder.Entity<VW_IMPRESSAO_WEB>()
                .Property(e => e.PEDIDO_CLIENTE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_LISOS_POR_COLECAO>()
                .Property(e => e.COR)
                .IsUnicode(false);

            modelBuilder.Entity<VW_LISOS_POR_COLECAO>()
                .Property(e => e.CAMINHO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_CLIENTE_TRANSP>()
                .Property(e => e.ID_TRANSP)
                .IsOptional();          

            modelBuilder.Entity<PRE_PEDIDO_CRITICA>()
                .Property(e => e.NUMERO_PRE_PEDIDO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO_CRITICA>()
                .Property(e => e.COD_CRITICA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO_CRITICA>()
                .Property(e => e.FLG_STATUS)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO_CRITICA>()
                .Property(e => e.COD_USU_JUSTIF)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO_CRITICA>()
                .Property(e => e.DES_JUSTIFICATIVA)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO_CRITICA>()
                .Property(e => e.ENVIA_EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO_CRITICA>()
                .Property(e => e.OBSERVACAO)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO_CRITICA>()
                .Property(e => e.VALOR_TAB)
                .HasPrecision(8, 2);

            modelBuilder.Entity<PRE_PEDIDO_CRITICA>()
                .Property(e => e.VALOR_ITEM)
                .HasPrecision(8, 2);

            modelBuilder.Entity<PRE_PEDIDO_CRITICA>()
                .Property(e => e.USUARIO)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO_CRITICA>()
                .Property(e => e.ID_CRITICA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_COLECAO>()
                .Property(e => e.NOME_COLECAO)
                .IsUnicode(false);

            modelBuilder.Entity<DISPONIBILIDADE_MALHA>()
                .Property(e => e.ARTIGO)
                .IsUnicode(false);

            modelBuilder.Entity<DISPONIBILIDADE_MALHA>()
                .Property(e => e.LISO_EST)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DISPONIBILIDADE_MALHA>()
                .Property(e => e.MAQUINA)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DISPONIBILIDADE_MALHA>()
                .Property(e => e.ID_DISP);

            modelBuilder.Entity<DISPONIBILIDADE_MALHA>()
                .Property(e => e.USUARIO)
                .IsUnicode(false);

            modelBuilder.Entity<DISPONIBILIDADE_MALHA>()
                .Property(e => e.SEMANA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CRIACAO_REDUZIDOS>()
                .Property(e => e.ID_CRIACAO_REDUZIDOS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CRIACAO_REDUZIDOS>()
                .Property(e => e.ARTIGO)
                .IsUnicode(false);

            modelBuilder.Entity<CRIACAO_REDUZIDOS>()
                .Property(e => e.DESENHO)
                .IsUnicode(false);

            modelBuilder.Entity<CRIACAO_REDUZIDOS>()
                .Property(e => e.VARIANTE)
                .IsUnicode(false);

            modelBuilder.Entity<CRIACAO_REDUZIDOS>()
                .Property(e => e.MAQUINA)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CRIACAO_REDUZIDOS>()
                .Property(e => e.REDUZIDO_CRIADO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PROXIMO_NUMERO_PEDIDO>()
                .Property(e => e.NUMERO_PEDIDO);

            modelBuilder.Entity<ARTIGO_PESO_PADRAO>()
                .Property(e => e.ARTIGO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARTIGO_PESO_PADRAO>()
                .Property(e => e.TECNOLOGIA)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARTIGO_PESO_PADRAO>()
                .Property(e => e.UM)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ARTIGO_PESO_PADRAO>()
                .Property(e => e.VALOR)
                .HasPrecision(6, 2);

            modelBuilder.Entity<ARTIGO_PESO_PADRAO>()
                .Property(e => e.USUARIO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_CONDICAO_PGTO>()
                .Property(e => e.DESCRI_COND)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO_ATEND>()
                .Property(e => e.COD_ATEND)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO_ATEND>()
                .Property(e => e.DESCRI_ATEND)
                .IsUnicode(false);

            modelBuilder.Entity<TAMANHOPECA>()
                .Property(e => e.CODIGO)
                .IsUnicode(false);

            modelBuilder.Entity<TAMANHOPECA>()
                .Property(e => e.DESCRICAO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_ARTIGOS_DISPONIVEIS>()
                .Property(e => e.ARTIGO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VW_ARTIGOS_DISPONIVEIS>()
                .Property(e => e.ID_TECNOLOGIA);

            modelBuilder.Entity<VW_ARTIGOS_DISPONIVEIS>()
                .Property(e => e.ID_CARAC_TEC)
                .IsOptional();

            modelBuilder.Entity<VW_ARTIGOS_DISPONIVEIS>()
                .Property(e => e.TECNOLOGIA)
                .IsUnicode(false);

            modelBuilder.Entity<VW_CARACT_DESENHOS>()
                .Property(e => e.DESENHO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_CARACT_DESENHOS>()
                .Property(e => e.ID_CARAC_TEC);

            modelBuilder.Entity<VW_CARACT_DESENHOS>()
                .Property(e => e.CARACT_TECNICA)
                .IsUnicode(false);

            modelBuilder.Entity<VW_CARACT_DESENHOS>()
                .Property(e => e.ID_TECNOLOGIA);

            modelBuilder.Entity<VW_CARACT_DESENHOS>()
                .Property(e => e.TECNOLOGIA)
                .IsUnicode(false);

            modelBuilder.Entity<VW_DESENHOS_POR_COLECAO>()
                .Property(e => e.COLECAO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_DESENHOS_POR_COLECAO>()
                .Property(e => e.DESENHO)
                .IsUnicode(false);

            modelBuilder.Entity<VW_DESENHOS_POR_COLECAO>()
                .Property(e => e.VARIANTE)
                .IsUnicode(false);

            modelBuilder.Entity<CONFIG_GERAL>()
                .Property(e => e.ID_CONFIG);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.ID_CONTROLE_DESENV)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.ID_CLIENTE)
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.CLIENTE_NOVO)
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.ID_STUDIO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.COD_STUDIO)
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.NOME_STUDIO)
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.STATUS_CRIACAO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.OBSERVACAO_CRIACAO)
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.ID_DESENHISTA_ART)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.DESENHO)
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.NOME_DESENHO)
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.STATUS_ART)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.OBSERVACAO_ART)
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.ID_COLORISTA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.STATUS_ENVIO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.OBSERVACAO_ENVIO)
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.STATUS_GERAL)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.MOTIVO_CANCELAMENTO)
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.USUARIO_CANCELAMENTO)
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.TEM_CRIACAO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.ID_ITEM_STUDIO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.ID_REP)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.ID_TEMA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.ID_DESENHO1)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.ID_DESENHO2)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.ID_TECNOLOGIA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.FATURAR)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.OBSERVACAO_ATEND)
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.ID_USUARIO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.ORIGEM)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.USUARIO_APROV_CANCEL)
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.MOTIVO_APROV_CANCEL)
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV>()
                .Property(e => e.GERA_PED_RESERVA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV_CARAC_TEC>()
                .Property(e => e.ID_CARAC_TEC)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV_CARAC_TEC>()
                .Property(e => e.CARACT_TECNICA)
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV_ITEM_STUDIO>()
                .Property(e => e.ID_ITEM_STUDIO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV_ITEM_STUDIO>()
                .Property(e => e.ID_STUDIO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV_ITEM_STUDIO>()
                .Property(e => e.COD_STUDIO)
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV_ITEM_STUDIO>()
                .Property(e => e.COD_DAL)
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV_ITEM_STUDIO>()
                .Property(e => e.ID_DESENHISTA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV_ITEM_STUDIO>()
                .Property(e => e.VALOR)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV_ITEM_STUDIO>()
                .Property(e => e.MOEDA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV_ITEM_STUDIO>()
                .Property(e => e.STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV_ITEM_STUDIO>()
                .Property(e => e.DESENHO)
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV_ITEM_STUDIO>()
                .Property(e => e.OLD_DAL)
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV_ITEM_STUDIO>()
                .Property(e => e.CLIENTE_COMPROU)
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV_ITEM_STUDIO>()
                .Property(e => e.OBSERVACAO)
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV_ITEM_STUDIO>()
                .Property(e => e.IMG_PGTO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CONTROLE_DESENV_TECNOLOGIA>()
                .Property(e => e.ID_TEC)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CONTROLE_DESENV_TECNOLOGIA>()
                .Property(e => e.DESC_TEC)
                .IsUnicode(false);

            modelBuilder.Entity<PED_ARTIGO_ATIVO>()
                .Property(e => e.ARTIGO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PED_ARTIGO_PROCESSO>()
                .Property(e => e.PROCESSO)
                .IsUnicode(false);

            modelBuilder.Entity<PED_TECNOLOGIA>()
                .Property(e => e.ID_TECNOLOGIA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PED_TECNOLOGIA>()
                .Property(e => e.TECNOLOGIA)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.NUMERO_PRE_PEDIDO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.NUMERO_PEDIDO_BLOCO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.ID_REPRESENTANTE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.ID_CLIENTE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.STATUS_PEDIDO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.CLIENTE_NOVO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.ESTADO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.TIPO_PEDIDO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.USUARIO_STATUS)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.QUALIDADE_COM)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.COD_COND_PGTO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.OBSERVACOES)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.NOME_CLIENTE)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.ORIGEM)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.ID_CLIENTE_ENTREGA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.ID_TRANSPORTADORA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.USUARIO_INICIO)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.USUARIO_FINAL)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.CNPJ)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.FLAG_DATA_OK_APS)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.ID_LOCAL)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.ID_STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.CNPJ_ENTREGA)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.GERENTE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.ATENDIMENTO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.NUMERO_CARTAO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.BANDEIRA)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.BANCO)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.MOTIVO_CANC)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.COMISSAO)
                .HasPrecision(4, 2);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.ID_CLI_FACCAO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.GRUPO_FACCAO)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO>()
                .Property(e => e.PEDIDO_CLIENTE)
                .HasPrecision(38, 0);
                           
            modelBuilder.Entity<USUARIOS>()
                .Property(e => e.COD_USU)
                .HasPrecision(38, 0);

            modelBuilder.Entity<USUARIOS>()
                .Property(e => e.NOME_USU)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIOS>()
                .Property(e => e.LOGIN_USU)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIOS>()
                .Property(e => e.SENHA_USU)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIOS>()
                .Property(e => e.ADMINISTRADOR)
                .HasPrecision(38, 0);

            modelBuilder.Entity<USUARIOS>()
                .Property(e => e.TIPO_USUARIO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<USUARIOS>()
                .Property(e => e.SETOR)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIOS>()
                .Property(e => e.SID_SIMULT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO_ITENS>()
                .Property(e => e.NUMERO_PRE_PEDIDO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO_ITENS>()
                .Property(e => e.NUMERO_PEDIDO_BLOCO);

            modelBuilder.Entity<PRE_PEDIDO_ITENS>()
                .Property(e => e.REDUZIDO_ITEM)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO_ITENS>()
                .Property(e => e.STATUS_ITEM)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO_ITENS>()
                .Property(e => e.LISO_ESTAMP)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO_ITENS>()
                .Property(e => e.MALHA_PLANO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO_ITENS>()
                .Property(e => e.MODA_DECORACAO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO_ITENS>()
                .Property(e => e.ARTIGO)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO_ITENS>()
                .Property(e => e.COR)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO_ITENS>()
                .Property(e => e.DESENHO)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO_ITENS>()
                .Property(e => e.VARIANTE)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO_ITENS>()
                .Property(e => e.QUANTIDADE)
                .HasPrecision(10, 2);

            modelBuilder.Entity<PRE_PEDIDO_ITENS>()
                .Property(e => e.PRECO_UNIT)
                .HasPrecision(10, 2);

            modelBuilder.Entity<PRE_PEDIDO_ITENS>()
                .Property(e => e.VALOR_TOTAL)
                .HasPrecision(10, 2);

            modelBuilder.Entity<PRE_PEDIDO_ITENS>()
                .Property(e => e.UM)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO_ITENS>()
                .Property(e => e.COLECAO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO_ITENS>()
                .Property(e => e.PE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO_ITENS>()
                .Property(e => e.ITEM);

            modelBuilder.Entity<PRE_PEDIDO_ITENS>()
                .Property(e => e.SIT_ITEM)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO_ITENS>()
                .Property(e => e.ORIGEM)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO_ITENS>()
                .Property(e => e.TROCA_TECNOLOGIA)
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO_ITENS>()
                .Property(e => e.COMPOSE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PRE_PEDIDO_ITENS>()
                .Property(e => e.COD_COMPOSE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO_ITENS>()
                .Property(e => e.ID_TAB_PRECO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO_ITENS>()
                .Property(e => e.QUALIDADE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO_ITENS>()
                .Property(e => e.PRECODIGITADOMOEDA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO_ITENS>()
                .Property(e => e.PRECOLISTA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PRE_PEDIDO_ITENS>()
                .Property(e => e.QTDEPC)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_PEDIDO_ROM_FAT>()
                 .Property(e => e.QTDE_PEDIDA)
                 .HasPrecision(38, 0);

            modelBuilder.Entity<VW_PEDIDO_ROM_FAT>()
                .Property(e => e.QTDE_ABERTA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_PEDIDO_ROM_FAT>()
                .Property(e => e.UM)
                .IsUnicode(false);

            modelBuilder.Entity<VW_PEDIDO_ROM_FAT>()
                .Property(e => e.COD_CLI)
                .IsUnicode(false);

            modelBuilder.Entity<VW_PEDIDO_ROM_FAT>()
                .Property(e => e.MES)
                .IsUnicode(false);

            modelBuilder.Entity<VW_PEDIDO_ROM_FAT>()
                .Property(e => e.PRECOUNITARIO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_PEDIDO_ROM_FAT>()
                .Property(e => e.ESTOQUE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VW_PEDIDO_ROM_FAT>()
                .Property(e => e.QTDE_FAT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_PEDIDO_ROM_FAT>()
                .Property(e => e.QTDE_ROM)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VW_PEDIDO_ROM_FAT>()
                .Property(e => e.OBSERV_PED)
                .IsUnicode(false);

            modelBuilder.Entity<ACOES>()
                .Property(e => e.ID_ACAO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ACOES>()
                .Property(e => e.DESCRICAO_ACAO)
                .IsUnicode(false);

            modelBuilder.Entity<ACOES>()
                .Property(e => e.UNIT)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIOS_ACOES>()
                .Property(e => e.ID_USUARIO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<USUARIOS_ACOES>()
                .Property(e => e.ID_ACAO)
                .HasPrecision(38, 0);

        }
    }
}
