﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ScimplyUI.UI.DTOs.Chart;
using ScimplyUI.UI.DTOs.User;
using System.Net.Http;

namespace ScimplyUI.UI.Controllers
{

    [ViewCheckFilter]
    public class ChartController : Controller
    {

        private readonly HttpClient _httpClient;

        public ChartController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Charts()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> GetUserCharts()
        {
			var accessToken = HttpContext.Request.Cookies["AccessToken"];

			var apiUrl = "https://localhost:7109/api/Admin/GetUserCharts";

			_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

			var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var strResponse = await response.Content.ReadAsStringAsync();

                var usersResponse = JsonConvert.DeserializeObject<AdminUserChartsQueryResponse>(strResponse);

                return Json(new {location = usersResponse.ChartLocationCountResponseDTO, totalUsers = usersResponse.TotalUsers, activeUsers = usersResponse.ActiveUsers, inactiveUsers = usersResponse.InactiveUsers});
            }
            return Json(new { message = "no response" });

        }

    }

}
