namespace Football.Infrastructure.Authentication;

public class Jwtoption
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }
    public int ExpirationInMinutes { get; set; }
}