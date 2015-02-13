namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.DISPONIBILIDADE_MALHA")]
    public partial class DISPONIBILIDADE_MALHA
    {
        public DateTime? DISPONIBILIDADE { get; set; }

        public DateTime? DISPONIBILIDADE_PCP { get; set; }

        public DateTime? SINCRONISMO { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(4)]
        public string ARTIGO { get; set; }

        [StringLength(1)]
        public string LISO_EST { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(1)]
        public string MAQUINA { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ID_DISP { get; set; }

        public DateTime? DATA_ATUALIZ { get; set; }

        [StringLength(30)]
        public string USUARIO { get; set; }

        public decimal? SEMANA { get; set; }
    }
}
