namespace Dalutex.Models.DataModels
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<VW_IMPRESSAO_WEB> VW_IMPRESSAO_WEB { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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
        }
    }
}
