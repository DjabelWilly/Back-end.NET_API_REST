using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Application.ViewModels
{
    public class RatingViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le Moody's Rating est obligatoire.")]
        [MaxLength(50, ErrorMessage = "Le Moody's Rating ne peut pas dépasser 50 caractères.")]
        public string MoodysRating { get; set; } = null!;

        [Required(ErrorMessage = "Le S&P Rating est obligatoire.")]
        [MaxLength(50, ErrorMessage = "Le S&P Rating ne peut pas dépasser 50 caractères.")]
        public string SandPRating { get; set; } = null!;

        [Required(ErrorMessage = "Le Fitch Rating est obligatoire.")]
        [MaxLength(50, ErrorMessage = "Le Fitch Rating ne peut pas dépasser 50 caractères.")]
        public string FitchRating { get; set; } = null!;

        [Range(0, 255, ErrorMessage = "OrderNumber doit être compris entre 0 et 255.")]
        public byte? OrderNumber { get; set; }
    }
}
