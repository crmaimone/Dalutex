namespace Dalutex.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TI_DALUTEX.USUARIOS")]
    public partial class USUARIOS
    {
        [Key]
        public decimal COD_USU { get; set; }

        [StringLength(25)]
        public string NOME_USU { get; set; }

        [StringLength(25)]
        public string LOGIN_USU { get; set; }

        [StringLength(20)]
        public string SENHA_USU { get; set; }

        public decimal? ADMINISTRADOR { get; set; }

        public decimal? TIPO_USUARIO { get; set; }

        public DateTime? DATA_CADASTRO { get; set; }

        public int? ID_REPRES { get; set; }

        [StringLength(30)]
        public string SETOR { get; set; }

        public decimal? SID_SIMULT { get; set; }

        [NotMapped]
        public bool PodeEditarPedidoNormal { get; set; }
        [NotMapped]
        public bool PodeEditarPedidoAvancado { get; set; }
        [NotMapped]
        public bool PodeCancelarItens { get; set; }

    }
}
