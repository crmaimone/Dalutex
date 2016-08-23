namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.TIPO_PEDIDO_USUARIO")]
    public partial class TIPO_PEDIDO_USUARIO
    {
        [Key]
        public int ID_TIPO_PEDIDO_USUARIO { get; set; }

        public int ID_USUARIO { get; set; }

        public int TIPO_PEDIDO { get; set; }
    }
}
