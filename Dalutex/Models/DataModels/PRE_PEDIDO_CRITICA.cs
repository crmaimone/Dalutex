namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.PRE_PEDIDO_CRITICA")]
    public partial class PRE_PEDIDO_CRITICA
    {
        public decimal? NUMERO_PRE_PEDIDO { get; set; }

        public decimal? COD_CRITICA { get; set; }

        public DateTime? DT_OCORR { get; set; }

        [StringLength(1)]
        public string FLG_STATUS { get; set; }

        public decimal? COD_USU_JUSTIF { get; set; }

        [StringLength(255)]
        public string DES_JUSTIFICATIVA { get; set; }

        [StringLength(1)]
        public string ENVIA_EMAIL { get; set; }

        [StringLength(255)]
        public string OBSERVACAO { get; set; }

        public int? ITEM_PEDIDO { get; set; }

        public decimal? VALOR_TAB { get; set; }

        public decimal? VALOR_ITEM { get; set; }

        [StringLength(100)]
        public string USUARIO { get; set; }

        [Key]
        public decimal ID_CRITICA { get; set; }
    }
}
