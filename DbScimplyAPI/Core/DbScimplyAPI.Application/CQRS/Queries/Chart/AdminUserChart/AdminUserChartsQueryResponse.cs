using DbScimplyAPI.Application.DTOs.Chart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.CQRS.Queries.Chart.AdminUserChart
{
    public class AdminUserChartsQueryResponse
    {
        public List<ChartLocationCountDTO> ChartLocationCountResponseDTO { get; set; }
        public List<ChartDateCountDTO> ChartMostCommonDateResponseDTO { get; set; }
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int InactiveUsers { get; set; }
    }
}
