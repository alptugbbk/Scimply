using DbScimplyAPI.Application.Abstractions.Cryptographies;
using DbScimplyAPI.Application.DTOs.User;
using DbScimplyAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.CQRS.Queries.User.AdminGetAllUsers
{
	public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, GetAllUsersQueryResponse>
	{

		private readonly IUserRepository _userRepository;
		private readonly IAESEncryption _aesEncryption;

        public GetAllUsersQueryHandler(IUserRepository userRepository, IAESEncryption aesEncryption)
        {
            _userRepository = userRepository;
            _aesEncryption = aesEncryption;
        }

        public async Task<GetAllUsersQueryResponse> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
		{

			var getAllUsers = _userRepository.GetAll();

			var users = getAllUsers.Select(x => new GetAllUsersResponseDTO
			{
				Status = x.Status,
				Id = x.Id,
				UserName = _aesEncryption.DecryptData(x.UserName),
				Password = x.Password,
				Meta = new MetaDTO
				{
					ResourceType = x.ResourceType,
					Created = x.Created,
					LastModified = x.LastModified,
					Version = _aesEncryption.DecryptData(x.Version),
					Location = _aesEncryption.DecryptData(x.Location),
				}
			}).ToList();

			return new GetAllUsersQueryResponse
			{
				GetAllUsersResponseDTO = users ?? new List<GetAllUsersResponseDTO>(),
				IsStatus = true,
			};

		}



	}
}
