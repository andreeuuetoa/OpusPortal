using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Base.Helpers;

public static class IdentityHelpers
{
    // extension function - User.GetUserId()
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        return Guid.Parse(
            user.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value
        );
    }

    public static string GenerateJWT(IEnumerable<Claim> claims, string key,
        string issuer, string audience,
        int expiresInSeconds)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddSeconds(expiresInSeconds);
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expires,
            signingCredentials: signingCredentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static bool IsTokenValid(string JWT, string key, string issuer, string audience,
        bool ignoreExpiration = true)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = GetValidationParameters(key, issuer, audience);

        try
        {
            tokenHandler.ValidateToken(JWT, validationParameters, out _);
        }
        catch (SecurityTokenExpiredException)
        {
            // Is it ok to be expired, since we are refreshing expired JWT?
            return ignoreExpiration;
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    private static TokenValidationParameters GetValidationParameters(string key,
        string issuer, string audience)
    {
        return new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ValidIssuer = issuer,
            ValidAudience = audience,
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true
        };
    }
}