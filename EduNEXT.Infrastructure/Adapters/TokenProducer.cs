using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Entities;
using EduNEXT.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EduNEXT.Infrastructure.Adapters;

public class TokenProducer : ITokenProducer
{
    private readonly TokenOptions _options;

    public TokenProducer(IOptions<TokenOptions> options)
    {
        _options = options.Value;
    }

    public string ProduceToken(Admin admin)
    {
        var claims = GetClaims(admin);
        
        var token = InternalProduce(claims);
        
        return token;
    }

    private static IEnumerable<Claim> GetClaims(Admin admin)
    {
        yield return new Claim(ClaimTypes.Name, admin.Name);
        yield return new Claim(ClaimTypes.Email, admin.Email.ToString());
        yield return new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString());
    }

    private string InternalProduce(IEnumerable<Claim> claims)
    {
        var now = DateTime.UtcNow;

        var expires = now.Add(TimeSpan.FromMinutes(_options.LifeTime));

        var jwt = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            notBefore: now,
            claims: claims,
            expires: expires,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_options.Secret)
                    ),  
                SecurityAlgorithms.HmacSha256)
        );
        
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return encodedJwt;
    }
}