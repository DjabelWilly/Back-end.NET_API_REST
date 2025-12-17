using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Application.ViewModels
{
    public class RatingViewModel
    {
        public int Id { get; set; }

        [Required] public string MoodysRating { get; set; } = null!;
        [Required] public string SandPRating { get; set; } = null!;
        [Required] public string FitchRating { get; set; } = null!;

        [Range(0, 255, ErrorMessage = "CurveId doit être entre 0 et 255")]
        public byte? OrderNumber { get; set; }
    }
}