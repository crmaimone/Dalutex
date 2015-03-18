namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.VW_ITENS_VALIDAR_RESERVA")]
    public partial class VW_ITENS_VALIDAR_RESERVA
    {
        public int? ID_CONTROLE { get; set; }

        public int? ID_REPRESENTANTE { get; set; }

        [StringLength(50)]
        public string COD_STUDIO { get; set; }

        [StringLength(10)]
        public string DESENHO { get; set; }

        [StringLength(50)]
        public string COD_DAL { get; set; }

        [StringLength(4)]
        public string VARIANTE { get; set; }

        [Key]
        [Column(Order = 0)]
        public int PEDIDO_RESERVA { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ID_VAR { get; set; }

        public int? ID_CLIENTE { get; set; }

        public int IT_PEDIDO_RES { get; set; }
    }
}
