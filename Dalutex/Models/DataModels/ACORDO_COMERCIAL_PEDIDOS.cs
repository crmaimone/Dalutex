namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.ACORDO_COMERCIAL_PEDIDOS")]
    public partial class ACORDO_COMERCIAL_PEDIDOS
    {
        [Key]
        public decimal ID_ACORDO_COM_PED { get; set; }

        public decimal ID_ACORDO { get; set; }

        public decimal PEDIDO { get; set; }

        public decimal? ITEM { get; set; }

        public decimal? QUANTIDADE { get; set; }
    }
}
