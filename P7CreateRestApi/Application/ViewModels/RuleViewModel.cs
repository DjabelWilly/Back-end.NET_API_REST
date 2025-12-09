using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Application.ViewModels
{
    public class RuleViewModel
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; } = null!;
        [Required] public string Description { get; set; } = null!;
        [Required] public string Json { get; set; } = null!;
        [Required] public string Template { get; set; } = null!;
        [Required] public string SqlStr { get; set; } = null!;
        [Required] public string SqlPart { get; set; } = null!;
    }
}
