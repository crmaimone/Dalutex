namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.PROXIMO_NUMERO_PEDIDO")]
    public partial class PROXIMO_NUMERO_PEDIDO
    {
        [Key]
        public int NUMERO_PEDIDO { get; set; }

        public int DISPONIVEL { get; set; }

        public DateTime? DATA_RESERVA_SID { get; set; }

        public DateTime? DATA_RESERVA_SGT { get; set; }
    }
}
