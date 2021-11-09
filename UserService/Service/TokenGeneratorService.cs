using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UserService.Service
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        public string GenerateJWTToken(string username, string role)
        {
            var claims = new[]
            {
                new Claim("username", username),
                new Claim("UserRole", role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("5U5h@n!1998_pr@n5hu"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            issuer: "UserWebApi",
            audience: "MovieTicketWebApi",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
            );

            var response = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                username = username
            };

            return JsonConvert.SerializeObject(response);
        }
    }
}
