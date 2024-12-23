using DbScimplyAPI.Application.CQRS.Commands.User.AdminCreateUser;
using DbScimplyAPI.Application.CQRS.Commands.User.AdminDeleteUser;
using DbScimplyAPI.Application.CQRS.Commands.User.AdminUpdateUser;
using DbScimplyAPI.Application.CQRS.Queries.Chart.AdminUserChart;
using DbScimplyAPI.Application.CQRS.Queries.User.AdminGetAllUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DbScimplyAPI.API.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {

        private readonly IMediator _mediator;


        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }




		[HttpPost("CreateUser")]
		public async Task<AdminCreateUserCommandResponse> CreateUser(AdminCreateUserCommandRequest request)
		{
			var response = await _mediator.Send(request);
			return response;
		}


        
		[HttpGet("GetAllUsers")]
        public async Task<GetAllUsersQueryResponse> GetAllUsers()
        {
            var response = await _mediator.Send(new GetAllUsersQueryRequest());
            return response;
        }



        [HttpPost("DeleteUser")]
        public async Task<AdminDeleteUserCommandResponse> DeleteUser(AdminDeleteUserCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return response;
        }



        [HttpPost("UpdateUser")]
        public async Task<AdminUpdateUserCommandResponse> UpdateUser(AdminUpdateUserCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return response;
        }



		[HttpGet("GetUserCharts")]
		public async Task<AdminUserChartsQueryResponse> GetUserCharts()
		{
			var response = await _mediator.Send(new AdminUserChartsQueryRequest());
			return response;
		}




	}
}

