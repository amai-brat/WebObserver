using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebObserver.Main.Application.Options;
using WebObserver.Main.Application.Services.Ifaces;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Entities;

namespace WebObserver.Main.Application.Services.Impls;

public class JwtService(IOptionsMonitor<JwtOptions> monitor) : IJwtService
{
    private readonly JwtOptions _jwtOptions = monitor.CurrentValue;
    
    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Jti, Guid.CreateVersion7().ToString()),
            new (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new (JwtRegisteredClaimNames.Name, user.Name),
            new (JwtRegisteredClaimNames.Email, user.Email),
        };
        
        if (_jwtOptions.AdminEmails.Contains(user.Email))
            claims.Add(new Claim(Roles.ClaimName, Roles.Admin));
            
        var now = DateTime.UtcNow;
        var jwt = new JwtSecurityToken(
            notBefore: now,
            claims: claims,
            expires: now.Add(TimeSpan.FromMinutes(_jwtOptions.AccessTokenLifetimeInMinutes)),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key)),
                SecurityAlgorithms.HmacSha256)
        );
        
        var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            
        return token;
    }
}