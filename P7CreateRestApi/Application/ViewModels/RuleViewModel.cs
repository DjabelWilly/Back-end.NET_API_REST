using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Application.ViewModels
{
    public class RuleViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom de la règle est obligatoire.")]
        [MaxLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "La description est obligatoire.")]
        [MaxLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères.")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Le JSON est obligatoire.")]
        public string Json { get; set; } = null!;

        [Required(ErrorMessage = "Le template est obligatoire.")]
        [MaxLength(500, ErrorMessage = "Le template ne peut pas dépasser 500 caractères.")]
        public string Template { get; set; } = null!;

        [Required(ErrorMessage = "La requête SQL est obligatoire.")]
        public string SqlStr { get; set; } = null!;

        [Required(ErrorMessage = "La partie SQL est obligatoire.")]
        public string SqlPart { get; set; } = null!;
    }
}
