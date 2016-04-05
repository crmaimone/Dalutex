
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dalutex.Models.DataModels
{

    [Table("TI_DALUTEX.PED_LINK_RESTRICAO_ARTIGO")]
    public partial class PED_LINK_RESTRICAO_ARTIGO
    {
        [Key]
        [Column(Order = 1)]    
        public int ID_CARAC_TEC { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ID_TECNOLOGIA { get; set; }

        [Key]
        [StringLength(4)]
        [Column(Order = 3)]    
        public string ARTIGO { get; set; }

        public override string ToString()
        {
            return ARTIGO + "-" + ID_CARAC_TEC.ToString() + "-" + ID_TECNOLOGIA.ToString();
        }
    }
}