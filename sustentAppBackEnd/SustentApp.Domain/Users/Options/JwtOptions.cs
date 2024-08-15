namespace SustentApp.Domain.Users.Options;

public class JwtOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Secret { get; set; }
    public int ExpiresInMinutes { get; set; }
}
