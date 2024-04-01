namespace WebApp.DTO;

public class JWTResponse
{
    public string Jwt { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}