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

        public enum ItemType 
        { 
            Estampado = 1,
            Liso = 2,
            Reserva = 3,
            ValidacaoReserva = 4
        }

        public enum CondicoesPagamento
        {
            CORTESIA = 432
        }

        public enum TiposPedido
        {
            VENDA = 0,
            BNFPROPRIO = 2,
            BNFTERCEIROS = 3,
            AMOSTRA = 6,
            PILOTAGEM = 7,
            REPOSICÃO = 9,
            BANCADO = 15,
            ESPECIAL = 16,
            RESERVA = 21
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
            Pocket = 12,
            Exclusivos = 23
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
            PedidoCompleto = 2, //OK
            ArtigoCompose = 3,
            CompletoPorArtigo = 4,
            PedidoIncompleto = 5
        }

        public enum Transportadoras
        {
            NossoCarro = 1
        }

        public enum GrupoColecoes
        {
            Pocket=1,
            Colecao=2,
            Exclusivos=3
        }

        public enum Moedas
        {
            Real = 0,
            Dollar = 2,
            Euro = 3,
            Marco = 4
        }
    }
}