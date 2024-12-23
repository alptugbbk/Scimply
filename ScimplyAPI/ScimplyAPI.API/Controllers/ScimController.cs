using Core.Abstractions.Services;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ScimplyAPI.API.Controllers
{

    [ApiController]
    [Route("scim/v2/Users")]
    public class ScimController : Controller
    {

        private readonly IScimService _scimService;


        public ScimController(IScimService scimService)
        {
            _scimService = scimService;
        }



        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDTO request)
        {

            var validationResult = await _scimService.ValidateScimSchema(request);

            if (validationResult.Status != 200)
            {
				return Json(new CreateUserResponseDTO
				{
					Schemas = validationResult.Schemas,
					Detail = validationResult.Detail,
					Status = validationResult.Status
				});
			}

            var result = await _scimService.CreateUserAsync(request);

            if(result.Status == 200)
            {
                return Json(new CreateUserResponseDTO
                {
                    Schemas = result.Schemas,
                    Detail = result.Detail,
                    Status = result.Status
                });
            }
            else
            {
                return Json(new CreateUserResponseDTO
                {
                    Schemas = result.Schemas,
                    Detail = result.Detail,
                    Status = result.Status
                });
            }

		}



    }

}
