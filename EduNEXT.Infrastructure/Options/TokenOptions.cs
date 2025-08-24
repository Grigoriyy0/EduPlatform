namespace EduNEXT.Infrastructure.Options;

public class TokenOptions
{
    public string Issuer { get; set; } = null!;

    public string Audience { get; set; } = null!;
    
    public int LifeTime { get; set; }
    
    public string Secret { get; set; } = null!;
}
