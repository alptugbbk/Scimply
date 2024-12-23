using DbScimplyAPI.Application.Abstractions.Token;
using DbScimplyAPI.Application.DTOs.Token;
using DbScimplyAPI.Domain.Entities.Admin;
using DbScimplyAPI.Domain.Entities.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DbScimplyAPI.Infrastructure.Token
{
    public class TokenHandler : ITokenHandler
    {

        private readonly IConfiguration _configuration;


        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        public TokenDTO CreateAccessToken(Admin admin ,User user, int minute)
        {
            TokenDTO tokenDTO = new();

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            tokenDTO.Expiration = DateTime.UtcNow.AddMinutes(minute);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, admin.Name),
                new Claim(ClaimTypes.NameIdentifier, admin.Id),
                new Claim(ClaimTypes.Role, "Admin")
            };



            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: tokenDTO.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: claims
                );

            JwtSecurityTokenHandler tokenHandler = new();

            tokenDTO.AccessToken = tokenHandler.WriteToken(securityToken);

            tokenDTO.RefreshToken = CreateRefreshToken();

            return tokenDTO;

        }



        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];

            using RandomNumberGenerator random = RandomNumberGenerator.Create();

            random.GetBytes(number);

            return Convert.ToBase64String(number);
        }


    }
}
