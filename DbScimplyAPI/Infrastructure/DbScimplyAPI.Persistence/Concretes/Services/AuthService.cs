using DbScimplyAPI.Application.Abstractions.Services;
using DbScimplyAPI.Application.Abstractions.Token;
using DbScimplyAPI.Application.DTOs.Token;
using DbScimplyAPI.Application.Repositories;
using DbScimplyAPI.Domain.Entities.Admin;
using DbScimplyAPI.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Persistence.Concretes.Services
{
    public class AuthService : IAuthService
    {

        private readonly IAdminRepository _adminRepository;
        private readonly ITokenHandler _tokenHandler;


        public AuthService(IAdminRepository adminRepository, ITokenHandler tokenHandler)
        {
            _adminRepository = adminRepository;
            _tokenHandler = tokenHandler;
        }



        public async Task<TokenDTO> RefreshTokenLoginAsync(string refreshToken)
        {
            
            Admin admin = await _adminRepository.GetSingleAsync(x => x.RefreshToken == refreshToken);

            if(admin != null && admin?.RefreshTokenEndDate > DateTime.UtcNow)
            {

                TokenDTO token = _tokenHandler.CreateAccessToken(admin,null,45);

                UpdateRefreshToken(token.RefreshToken, admin, null, token.Expiration, 60);

                return token;
            }

            throw new Exception("failed");


        }



        public async Task UpdateRefreshToken(string refreshToken, Admin? admin, User? user, DateTime accessTokenDate, int addOnAccessTokenDate)
        {

            if(admin != null)
            {
                admin.RefreshToken = refreshToken;
                admin.RefreshTokenEndDate = accessTokenDate.AddMinutes(addOnAccessTokenDate);
                await _adminRepository.UpdateAsync(admin);
            }

            throw new Exception("failed");


        }


    }
}
