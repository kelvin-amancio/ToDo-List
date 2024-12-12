using System.ComponentModel.DataAnnotations;

namespace ToDoApi.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome de usuário deve ter entre {2} e {1} caracteres.")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(200, MinimumLength = 6, ErrorMessage = "A senha deve ter no mínimo {2} caracteres.")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "A confirmação da senha é obrigatória.")]
        [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
