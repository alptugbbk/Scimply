using DbScimplyAPI.Application.Abstractions.Cryptographies;
using DbScimplyAPI.Application.DTOs.Chart;
using DbScimplyAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.CQRS.Queries.Chart.AdminUserChart
{
    public class AdminUserChartsQueryHandler : IRequestHandler<AdminUserChartsQueryRequest, AdminUserChartsQueryResponse>
    {

        private readonly IUserRepository _userRepository;
        private readonly IAESEncryption _aesEncryption;

        public AdminUserChartsQueryHandler(IUserRepository userRepository, IAESEncryption aesEncryption)
        {
            _userRepository = userRepository;
            _aesEncryption = aesEncryption;
        }

        public async Task<AdminUserChartsQueryResponse> Handle(AdminUserChartsQueryRequest request, CancellationToken cancellationToken)
        {

            var users = _userRepository.GetAll(false).ToList();

            var totalUsers = users.Count();

            var activeUsers = users.Count(x => x.Status);

            var inactiveUsers = users.Count(x => !x.Status);

            // location
            string GetDomain(string url)
            {
                var match = Regex.Match(url, @"^(https?:\/\/[^\/]+)");
                return match.Success ? match.Value : url;
            }

            var locationCounts = users.GroupBy(x => GetDomain(_aesEncryption.DecryptData(x.Location))).Select(x => new ChartLocationCountDTO
            {
                Location = x.Key,
                Count = x.Count()
            }).OrderBy(x => x.Count).ToList();

            // most common date
            var userCountsByMonth = users.GroupBy(x => x.Created.ToString("yyyy-MM"))
            .Select(x => new ChartDateCountDTO
            {
                Date = x.Key,
                Count = x.Count()
            }).OrderBy(x=> x.Date).ToList();

            return new AdminUserChartsQueryResponse
            {
                ChartLocationCountResponseDTO = locationCounts,
                ChartMostCommonDateResponseDTO = userCountsByMonth,
                TotalUsers = totalUsers,
                ActiveUsers = activeUsers,
                InactiveUsers = inactiveUsers
            };

        }


    }
}
