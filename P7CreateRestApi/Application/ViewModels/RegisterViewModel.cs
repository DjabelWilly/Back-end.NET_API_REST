using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Application.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(8)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).+$",
            ErrorMessage = "Le mot de passe doit contenir au moins une majuscule, un chiffre et un symbole.")]
        public string Password { get; set; } = null!;

        [Required]
        public string FullName { get; set; } = null!;
    }
}
