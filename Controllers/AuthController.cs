using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtTokenHelper _tokenService;

    public AuthController(JwtTokenHelper tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        // Mock authentication (replace with real validation logic)
        if (model.Username == "admin" && model.Password == "password")
        {
            var token = _tokenService.GenerateToken(model.Username, "Admin");
            return Ok(new { Token = token });
        }

        return Unauthorized("Invalid credentials");
    }
}

public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}
