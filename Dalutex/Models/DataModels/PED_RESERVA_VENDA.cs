namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.PED_RESERVA_VENDA")]
    public partial class PED_RESERVA_VENDA
    {
        public int PEDIDO_RESERVA { get; set; }

        public int ITEM_PED_RESERVA { get; set; }

        public int ID_VAR_PED_RESERVA { get; set; }

        public int PEDIDO_VENDA { get; set; }

        public int ITEM_PED_VENDA { get; set; }

        [Key]
        public decimal ID_PED_RESERVA_VENDA { get; set; }
    }
}
