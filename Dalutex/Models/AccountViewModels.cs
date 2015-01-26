using System.ComponentModel.DataAnnotations;

namespace Dalutex.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Lembre-se de mim!")]
        public bool RememberMe { get; set; }
    }

}
