using HackathonPonto.Application.InputModels;
using HackathonPonto.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace HackathonPonto.Application.Services
{
    public static class TokenService
    {
        public static string GenerateToken(string cpf, string perfil)
        {

            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            var claims = new List<Claim> { };
            claims.Add(new Claim("Cpf", cpf));
            claims.Add(new Claim(ClaimTypes.Role, perfil));

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
