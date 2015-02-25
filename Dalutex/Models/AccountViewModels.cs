using System.ComponentModel.DataAnnotations;

namespace Dalutex.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "LOGIN")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "SENHA")]
        public string Password { get; set; }

        [Display(Name = "LEMBRAR SENHA")]
        public bool RememberMe { get; set; }
    }

}
