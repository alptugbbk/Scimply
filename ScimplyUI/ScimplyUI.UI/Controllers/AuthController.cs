using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ScimplyUI.UI.DTOs.Auth;
using ScimplyUI.UI.Models.Auth;
using System.Text;

namespace ScimplyUI.UI.Controllers
{
	public class AuthController : Controller
	{

		private readonly HttpClient _httpClient;
		private readonly IConfiguration _configuration;

		public AuthController(HttpClient httpClient, IConfiguration configuration)
		{
			_httpClient = httpClient;
			_configuration = configuration;
		}



		public IActionResult Login()
		{
			return View();
		}



		[HttpPost]
		public async Task<IActionResult> Login([FromBody] AdminLoginViewModel adminLoginViewModel)
		{

			var baseUrl = _configuration["SubmitUrl:DbscimplyAPI"];

			var apiUrl = $"{baseUrl}/api/auth/loginadmin";

			var newModel = new
			{
				LoginAdminRequestDTO = adminLoginViewModel
			};

			var convert = JsonConvert.SerializeObject(newModel);

			var content = new StringContent(convert, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync(apiUrl, content);

			var strResponse = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
			{
				var successResponse = JsonConvert.DeserializeObject<LoginResponseDTO>(strResponse);

				if (successResponse.IsStatus == true)
				{
					if (successResponse.IsStatus == true)
					{
						if (!string.IsNullOrEmpty(successResponse.AccessToken))
						{
							HttpContext.Response.Cookies.Append("AccessToken", successResponse.AccessToken, new CookieOptions
							{
								HttpOnly = true,
								Secure = true,
								SameSite = SameSiteMode.Strict,
								Expires = DateTime.UtcNow.AddMinutes(45)
							});
						}

						if (!string.IsNullOrEmpty(successResponse.RefreshToken))
						{
							HttpContext.Response.Cookies.Append("RefreshToken", successResponse.RefreshToken, new CookieOptions
							{
								HttpOnly = true,
								Secure = true,
								SameSite = SameSiteMode.Strict,
								Expires = DateTime.UtcNow.AddMinutes(60)
							});
						}

						return Json(new { message = successResponse.Message, status = successResponse.IsStatus });
					}
					else
					{
						return Json(new { message = successResponse.Message, status = successResponse.IsStatus });
					}

				}

			}

			return View();

		}



		[HttpPost]
		public async Task<IActionResult> RefreshToken()
		{
			var refreshToken = HttpContext.Request.Cookies["RefreshToken"];

			if (string.IsNullOrEmpty(refreshToken))
			{
				return null;
			}
			var baseUrl = _configuration["SubmitUrl:DbscimplyAPI"];

			var apiUrl = $"{baseUrl}/api/auth/refreshtoken";

			var convert = JsonConvert.SerializeObject(new { RefreshToken = refreshToken });

			var content = new StringContent(convert, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync(apiUrl, content);

			var strResponse = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
			{

				var successResponse = JsonConvert.DeserializeObject<LoginResponseDTO>(strResponse);

				Response.Cookies.Append("AccessToken", successResponse.AccessToken, new CookieOptions
				{
					HttpOnly = true,
					Secure = true,
					SameSite = SameSiteMode.Strict,
					Expires = DateTime.UtcNow.AddMinutes(60)
				});

				return Json(new { message = successResponse.Message });

			}

			var errorResponse = JsonConvert.DeserializeObject<LoginResponseDTO>(strResponse);

			return Json(new { message = errorResponse.Message });

		}


	}
}
