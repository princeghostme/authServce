using Contracts;
using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Services
{
    public class JwtService : IJwtService
    {
        private readonly string secretkey = "qwertyuiopkjhgfdsalzxcvbnm1234567890asdfghjkiuytrexcvb";
        private readonly SymmetricSecurityKey key;
        private readonly SigningCredentials creds;

        public JwtService()
        {
            key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));
            creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }

        public async Task<string> encode(TokenDetail userData)
        {
            try
            {
                var tokenDetail = JsonSerializer.Serialize(userData);
                var t = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(tokenDetail);
                var handler = new JwtSecurityTokenHandler();
                var token = handler.CreateJwtSecurityToken(
                    issuer : "yourdomain.com",
                    audience: "yourdomain.com",
                    subject: null,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                );
                
                token.Payload.Add("detail" , t);

                string _token = new JwtSecurityTokenHandler().WriteToken(token);

                return _token;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TokenDetail> decode(string Token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(Token);
            jwtSecurityToken.Payload.TryGetValue("detail",out object? obj);

            var dtl = JsonSerializer.Deserialize<TokenDetail>(JsonSerializer.Serialize(obj));

            return dtl;

        }
    }
}
