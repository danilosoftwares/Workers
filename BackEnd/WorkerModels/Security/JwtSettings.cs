namespace WorkerModels.Security;
public class JwtSettings:IJwtSettings
{
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int ExpirationSeconds { get; set; }
}
