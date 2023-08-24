using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using MyChatAppApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MyChatAppApi.Utilites
{
    public class CommonUtillites
    {
        private readonly IHttpContextAccessor  _contextAccessor;


        public CommonUtillites(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetTokenFromRequest()
        {
            return _contextAccessor.HttpContext.Request.Query["access_Token"];
        }


        public Guid GetUserID()
        {
            var claims = ReadUserClaims(GetTokenFromRequest()).ToArray();

            return Guid.Parse(claims[2].Value);
        }

        public string? GetUserName()
        {
            var claims = ReadUserClaims(GetTokenFromRequest()).ToArray();

            if (claims.Length == 0) return null;

            return claims[0].Value;
        }

        public IEnumerable<Claim> ReadUserClaims(string token)
        {
            var JWTReader = new JwtSecurityTokenHandler();
            if(token == null) return new List<Claim>();
            var Token = JWTReader.ReadToken(token) as JwtSecurityToken;


            return Token.Claims;
        }



        public string GenerateJWT(Client user)
        {
            var key = Encoding.UTF8.GetBytes(GenerateRandomSecretKey(1200));

            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.Name, user.UserName),
                 new Claim(ClaimTypes.Email, user.Email),
                 new Claim("Sid", user.Id.ToString()),
                 new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddHours(2).ToShortTimeString())
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1), // Set expiration time
                SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;

        }

        public string GenerateRandomSecretKey(int length)
        {
            using var rng = new RNGCryptoServiceProvider();
            var keyBytes = new byte[length];
            rng.GetBytes(keyBytes);
            return Convert.ToBase64String(keyBytes);
        }
    }
}
