using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ScimplyUI.UI.DTOs.User;
using ScimplyUI.UI.Models.User;
using System.Security.AccessControl;
using System.Text;

namespace ScimplyUI.UI.Controllers
{

    [SessionCheckFilter]
    public class HomeController : Controller
    {

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;


        public HomeController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> GetAllUsers()
        {

            var userId = HttpContext.Session.GetString("UserId");

            var baseUrl = _configuration["SubmitUrl:DbscimplyAPI"];

            var apiUrl = $"{baseUrl}/api/Admin/GetAllUsers";

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, apiUrl);

            httpRequestMessage.Headers.Add("UserId", userId);

            var response = await _httpClient.SendAsync(httpRequestMessage);

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


        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserViewModel userViewModel)
        {

            var userId = HttpContext.Session.GetString("UserId");

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

            var baseUrl = _configuration["SubmitUrl:DbscimplyAPI"];

            var apiUrl = $"{baseUrl}/api/Admin/CreateUser";

            var convert = JsonConvert.SerializeObject(newModel);

            var content = new StringContent(convert, Encoding.UTF8, "application/json");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, apiUrl)
            {
                Content = content
            };

            httpRequestMessage.Headers.Add("UserId", HttpContext.Session.GetString("UserId"));

            var response = await _httpClient.SendAsync(httpRequestMessage);

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

            var userId = HttpContext.Session.GetString("UserId");

            var deleteUserRequestDto = new DeleteUserRequestDTO
            {
                UserId = id,
            };

            var baseUrl = _configuration["SubmitUrl:DbscimplyAPI"];

            var apiUrl = $"{baseUrl}/api/Admin/DeleteUser";

            var convert = JsonConvert.SerializeObject(deleteUserRequestDto);

            var content = new StringContent(convert, Encoding.UTF8, "application/json");


            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, apiUrl)
            {
                Content = content
            };

            httpRequestMessage.Headers.Add("UserId", HttpContext.Session.GetString("UserId"));

            var response = await _httpClient.SendAsync(httpRequestMessage);

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

            var userId = HttpContext.Session.GetString("UserId");

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

            var baseUrl = _configuration["SubmitUrl:DbscimplyAPI"];

            var apiUrl = $"{baseUrl}/api/Admin/UpdateUser";

            var convert = JsonConvert.SerializeObject(newModel);

            var content = new StringContent(convert, Encoding.UTF8, "application/json");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, apiUrl)
            {
                Content = content
            };

            httpRequestMessage.Headers.Add("UserId", HttpContext.Session.GetString("UserId"));

            var response = await _httpClient.SendAsync(httpRequestMessage);

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
