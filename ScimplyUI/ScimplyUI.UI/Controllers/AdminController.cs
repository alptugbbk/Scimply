using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ScimplyUI.UI.DTOs.User;
using ScimplyUI.UI.Models.User;
using System.Security.AccessControl;
using System.Text;

namespace ScimplyUI.UI.Controllers
{

	[ViewCheckFilter]
	public class AdminController : Controller
	{

		private readonly HttpClient _httpClient;


		public AdminController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}



		public IActionResult Index()
		{
			return View();
		}



		[HttpPost]
		public async Task<IActionResult> GetAllUsers()
		{

			var accessToken = HttpContext.Request.Cookies["AccessToken"];

			var apiUrl = "https://localhost:7109/api/Admin/GetAllUsers";

			if (accessToken != null)
			{

				_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

				var response = await _httpClient.GetAsync(apiUrl);

				var strResponse = await response.Content.ReadAsStringAsync();

				if (response.IsSuccessStatusCode)
				{
					var usersResponse = JsonConvert.DeserializeObject<GetAllUsersQueryResponse>(strResponse);
					return Json(usersResponse.GetAllUsersResponseDTO);
				}
				else
				{
					return Json(new { message = "No response", statusCode = response.StatusCode });
				}

			}

			return Json(new { message = "Request failed" });
		}



		[HttpPost]
		public async Task<IActionResult> CreateUser([FromBody] UserViewModel userViewModel)
		{

			var accessToken = HttpContext.Request.Cookies["AccessToken"];

			var createUserRequestDTO = new CreateUserRequestDTO()
			{
				Schemas = new[] { userViewModel.Schemas },
				UserName = userViewModel.UserName,
				Password = userViewModel.Password,
				ResourceType = userViewModel.ResourceType,
				Version = userViewModel.Version,
			};

			var newModel = new
			{
				CreateUserRequestDTO = createUserRequestDTO,
			};

			var apiUrl = "https://localhost:7109/api/Admin/CreateUser";

			var convert = JsonConvert.SerializeObject(newModel);

			var content = new StringContent(convert, Encoding.UTF8, "application/json");

			_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

			var response = await _httpClient.PostAsync(apiUrl, content);

			var strResponse = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
			{
				var responseData = JsonConvert.DeserializeObject<CreateUserResponseDTO>(strResponse);

				if (responseData.Status == 200)
				{
					return Json(new { message = responseData.Detail, status = responseData.Status });
				}
				else
				{
					return Json(new { message = responseData.Detail, status = responseData.Status });
				}

			}

			return Json(new { });

		}



		[HttpPost]
		public async Task<IActionResult> DeleteUser(string id)
		{
			var accessToken = HttpContext.Request.Cookies["AccessToken"];

			var deleteUserRequestDto = new DeleteUserRequestDTO
			{
				UserId = id,
			};

			var apiUrl = "https://localhost:7109/api/Admin/DeleteUser";

			var convert = JsonConvert.SerializeObject(deleteUserRequestDto);

			var content = new StringContent(convert, Encoding.UTF8, "application/json");

			_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

			var response = await _httpClient.PostAsync(apiUrl, content);

			var strResponse = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
			{
				var responseData = JsonConvert.DeserializeObject<DeleteUserResponseDTO>(strResponse);

				if (responseData.IsStatus = true)
				{
					return Json(new { message = responseData.Message, status = responseData.IsStatus });
				}
				else
				{
					return Json(new { message = responseData.Message, status = responseData.IsStatus });
				}

			}

			return Json(new { });

		}



		[HttpPost]
		public async Task<IActionResult> UpdateUser([FromBody] UserViewModel userViewModel)
		{

			var accessToken = HttpContext.Request.Cookies["AccessToken"];

			var updateUserRequestDTO = new UpdateUserRequestDTO
			{
				Id = userViewModel.Id,
				UserName = userViewModel.UserName,
				Status = userViewModel.Status,
				ResourceType = userViewModel.ResourceType,
				Version = userViewModel.Version,
				Location = userViewModel.Location,

			};

			var newModel = new
			{
				UpdateUserRequestDTO = updateUserRequestDTO,
			};

			var apiUrl = "https://localhost:7109/api/Admin/UpdateUser";

			var convert = JsonConvert.SerializeObject(newModel);

			var content = new StringContent(convert, Encoding.UTF8, "application/json");

			_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

			var response = await _httpClient.PostAsync(apiUrl, content);

			var strResponse = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
			{
				var responseData = JsonConvert.DeserializeObject<UpdateUserResponseDTO>(strResponse);

				if (responseData.Status == 200)
				{
					return Json(new { message = responseData.Detail, status = responseData.Status });
				}
				else
				{
					return Json(new { message = responseData.Detail, status = responseData.Status });
				}

			}

			return Json(new { });
		}




	}
}
