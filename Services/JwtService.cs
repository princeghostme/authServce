using Contracts;
using Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class JwtService : IJwtService
    {
        private readonly string _secretKey = "your-secret-key-here-change-in-production"; // Should be in appsettings
        private readonly string _issuer = "auth-service";
        private readonly string _audience = "auth-service-users";

        public Task<string> encode(TokenDetail tokenDetail)
        {
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.GivenName, tokenDetail.firstName ?? ""),
                    new Claim(ClaimTypes.Surname, tokenDetail.lastName ?? ""),
                    new Claim(ClaimTypes.Email, tokenDetail.Email ?? "")
                };

                var token = new JwtSecurityToken(
                    issuer: _issuer,
                    audience: _audience,
                    claims: claims,
                    expires: tokenDetail.ExpiresAt,
                    signingCredentials: credentials
                );

                var tokenHandler = new JwtSecurityTokenHandler();
                var encodedToken = tokenHandler.WriteToken(token);

                return Task.FromResult(encodedToken);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error encoding JWT: {ex.Message}", ex);
            }
        }

        public Task<TokenDetail?> decode(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_secretKey);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _issuer,
                    ValidateAudience = true,
                    ValidAudience = _audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var tokenDetail = new TokenDetail
                {
                    firstName = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value,
                    lastName = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value,
                    Email = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                    IssuedAt = jwtToken.ValidFrom,
                    ExpiresAt = jwtToken.ValidTo
                };

                return Task.FromResult<TokenDetail?>(tokenDetail);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error decoding JWT: {ex.Message}", ex);
            }
        }
    }
}
