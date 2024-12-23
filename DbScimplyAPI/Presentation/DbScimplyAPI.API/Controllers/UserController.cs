using DbScimplyAPI.Application.CQRS.Commands.User.ScimCreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DbScimplyAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {

        private readonly IMediator _mediator;


        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpPost("CreateUser")]
        public async Task<ScimCreateUserCommandResponse> CreateUser(ScimCreateUserCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return response;
        }



    }
}
