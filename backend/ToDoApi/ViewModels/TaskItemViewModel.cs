using System.ComponentModel.DataAnnotations;

namespace ToDoApi.ViewModels
{
    public class TaskItemViewModel
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O título deve ter entre {2} e {1} caracteres.")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "A descrição deve ter entre {2} e {1} caracteres.")]
        public string Description { get; set; } = string.Empty;
        public bool Completed { get; set; }
    }
}
