using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Application.ViewModels
{
    public class UserViewModel
    {
        [Required] public string Username { get; set; } = null!;
        [Required] public string Password { get; set; } = null!;
        [Required] public string Fullname { get; set; } = null!;
        [Required] public string Role { get; set; } = null!;
    }
}
