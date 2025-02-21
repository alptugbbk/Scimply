using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ScimplyUI.UI.DTOs.Auth;
using ScimplyUI.UI.Models.Auth;
using System.Security.Cryptography.X509Certificates;
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


		public IActionResult ForgotPassword()
		{
			return View();
		}


		public IActionResult ResetPassword(string userId)
		{
			if (string.IsNullOrEmpty(userId))
			{
				return RedirectToAction("ForgotPassword", "Auth");
			}

			return View(new ForgotPasswordViewModel { UserId = userId });
		}


		[HttpPost]
		public async Task<IActionResult> Login([FromBody] AdminLoginViewModel adminLoginViewModel)
		{

			var baseUrl = _configuration["SubmitUrl:DbscimplyAPI"];

			var apiUrl = $"{baseUrl}/api/auth/adminlogin";

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

				var successResponse = JsonConvert.DeserializeObject<AdminLoginResponseDTO>(strResponse);

				if (successResponse.IsTwoFactor == true)
				{
					return Json(new { message = successResponse.Message, isTwoFactor = successResponse.IsTwoFactor, id = successResponse.Id });
				}

				if (successResponse.Status == 200)
				{

					if (adminLoginViewModel.RememberMe)
					{
						var cookie = new CookieOptions
						{
							Expires = DateTimeOffset.UtcNow.AddDays(10),
							HttpOnly = true,
							Secure = true
						};
						Response.Cookies.Append("UserId", adminLoginViewModel.UserName, cookie);
					}

					HttpContext.Session.SetString("UserId", successResponse.Id.ToString());
                    return Json(new { message = successResponse.Message, status = successResponse.Status });

				}
				else
				{
					return Json(new { message = successResponse.Message, status = successResponse.Status });
				}

			}

			return View();

		}


		[HttpPost]
		public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordViewModel forgotPasswordViewModel)
		{
            var baseUrl = _configuration["SubmitUrl:DbscimplyAPI"];

            var apiUrl = $"{baseUrl}/api/auth/adminforgotpassword";

			var request = new ForgotPasswordRequestDTO
			{
				Email = forgotPasswordViewModel.Email,
			};

			var convert = JsonConvert.SerializeObject(request);

			var content = new StringContent(convert, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync(apiUrl, content);

			var strResponse = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
			{
				var successResponse = JsonConvert.DeserializeObject<AdminForgotPasswordQueryResponse>(strResponse);

                if (successResponse.Status == 200)
                {
                    return Json(new { message = successResponse.Message, status = successResponse.Status });
                }
                else
                {
                    return Json(new { message = successResponse.Message, status = successResponse.Status });
                }
            }

			return View();
        }


		[HttpPost]
		public async Task<IActionResult> ResetPassword([FromBody] ForgotPasswordViewModel forgotPasswordViewModel)
		{
            var baseUrl = _configuration["SubmitUrl:DbscimplyAPI"];

            var apiUrl = $"{baseUrl}/api/auth/adminresetpassword";

			var resetPasswordRequestDto = new ResetPasswordRequestDTO
			{
				UserId = forgotPasswordViewModel.UserId,
				NewPassword = forgotPasswordViewModel.NewPassword
			};

			var newModel = new
			{
                ResetPasswordRequestDTO = resetPasswordRequestDto
            };

            var convert = JsonConvert.SerializeObject(newModel);

            var content = new StringContent(convert, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiUrl, content);

            var strResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var successResponse = JsonConvert.DeserializeObject<AdminForgotPasswordQueryResponse>(strResponse);

                if (successResponse.Status == 200)
                {
                    return Json(new { message = successResponse.Message, status = successResponse.Status });
                }
                else
                {
                    return Json(new { message = successResponse.Message, status = successResponse.Status });
                }
            }

            return View();
        }


		[HttpPost]
		public async Task<IActionResult> TwoFactor([FromBody] AdminLoginViewModel adminLoginViewModel)
		{

			var baseUrl = _configuration["SubmitUrl:DbscimplyAPI"];

			var apiUrl = $"{baseUrl}/api/auth/twofactor";

			var twoFactorRequestDto = new TwoFactorRequestDTO
			{
				Code = adminLoginViewModel.Code,
				Id = adminLoginViewModel.Id
			};

			var request = new
			{
				TwoFactorRequestDTO = twoFactorRequestDto
			};

			var convert = JsonConvert.SerializeObject(request);

			var content = new StringContent(convert, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync(apiUrl, content);

			var strResponse = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
			{
				var successResponse = JsonConvert.DeserializeObject<AdminLoginResponseDTO>(strResponse);

				if (successResponse.Status == 200)
				{
					HttpContext.Session.SetString("UserId", request.TwoFactorRequestDTO.Id);
					return Json(new { message = successResponse.Message, status = successResponse.Status });
				}
				else
				{
					return Json(new { message = successResponse.Message, status = successResponse.Status });
				}

			}

			return View();
		}


		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			HttpContext.Session.Clear();
			
			foreach(var cookie in Request.Cookies.Keys)
			{
				Response.Cookies.Delete(cookie);
			}

			return Json(new { redirectUrl = Url.Action("Login", "Auth") });

		}


	}
}
