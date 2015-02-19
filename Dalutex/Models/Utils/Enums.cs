using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dalutex.Models
{
    public class Enums
    {
        public enum CanaisVenda
        {
            MERCADO_INTERNO = 1,
            MERCADO_EXTERNO = 2,
            AMOSTRAS_TRANSFER =3,
            TELEVENDAS = 4,
            DESFILE = 5,
            CARTELAS = 7,
            CARTAO_BNDES = 9,
            AMOSTRA_EXCLUSIVA = 10,
            TELEVENDAS_VIA_BNDES = 11,
            POCKET = 15,
            AMOSTRAS_MALHARIA = 16,
            DESENVOLVIMENTO = 17
        }

        public enum QualidadeComercial
        {
            A = 0,
            B = 1,
            C = 2
        }

        public enum FatorMultiplicacaoQualidadeComercial
        {
            A = 1,
            B = 2,
            C = 4
        }

        public enum TipoColecaoEspecial
        {
            Atual = 5,
            Pocket = 12
        }

        public enum ValorPadraoUnidade
        {
            Quilo = 19,
            Metro = 100
        }

        public enum TiposCritica
        {
            LiberacaoFinanceira = 7,
            SemReduzido = 17,
            PrecoDiferente = 2
        }

        public enum TiposAtendimento
        {
            EstampaCompleta = 1,

            //(VlTotalPedido * QualidadeCml)/numero parcelas | >=500 Ok
            //Se for a vista? verificar com Odair
            PedidoCompleto = 2,
            ArtigoCompose = 3,
            CompletoPorArtigo = 4,
            PedidoIncompleto = 5
        }
    }
}