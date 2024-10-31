using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WorkerModels.Interfaces.Security;
using WorkerModels.Model;
using WorkerModels.Security;

namespace WorkerSecurity.Authentication;

public class TokenService : ITokenService
{
    private readonly IJwtSettings _jwtSettings;

    public TokenService(JwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }

    public ResultToken CreateJwtToken(UserDTO user)
    {
        ResultToken tokenModel = new ResultToken();
        tokenModel.expires_in = _jwtSettings.ExpirationSeconds;
        
        var secretkey = Encoding.UTF8.GetBytes(_jwtSettings.Secret);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("identification", user.Id.ToString()),
        };

        SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(secretkey);
        SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddSeconds(_jwtSettings.ExpirationSeconds),
            signingCredentials: signingCredentials
        );
        tokenModel.access_token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return tokenModel;
    }
}