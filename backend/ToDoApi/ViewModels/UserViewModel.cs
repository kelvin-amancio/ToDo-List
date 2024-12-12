using System.ComponentModel.DataAnnotations;

namespace ToDoApi.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome de usuário deve ter entre {2} e {1} caracteres.")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(200, MinimumLength = 6, ErrorMessage = "A senha deve ter no mínimo {2} caracteres.")]
        public string Password { get; set; } = string.Empty;
    }
}
