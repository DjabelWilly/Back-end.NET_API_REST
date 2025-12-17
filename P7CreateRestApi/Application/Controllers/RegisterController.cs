using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Entities;

[ApiController]
[Route("register")]
public class RegisterController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public RegisterController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            FullName = model.FullName
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        await _userManager.AddToRoleAsync(user, "User");

        return Ok("User created");
    }
}
