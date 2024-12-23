using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DbScimplyAPI.API
{
    public class AuthMiddleware
    {

        private readonly RequestDelegate _next;

		public AuthMiddleware(RequestDelegate next)
		{
			_next = next;
		}



		public async Task InvokeAsync(HttpContext context)
		{

			try
			{

				if (context.Request.Path.StartsWithSegments("/api/auht"))
				{
					await _next(context);
					return;
				}

				var accessToken = context.Session.GetString("AccessToken");

				if (string.IsNullOrEmpty(accessToken))
				{
					context.Response.StatusCode = StatusCodes.Status401Unauthorized;
					await context.Response.WriteAsync("Access token is missing.");
					return;
				}

				var tokenHandler = new JwtSecurityTokenHandler();

				var jwtToken = tokenHandler.ReadJwtToken(accessToken);

				var roleClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);

				if (roleClaim != null)
				{

					var userRole = roleClaim.Value;


					if (userRole == "Admin")
					{

						await _next(context);
					}
					else
					{
						context.Response.StatusCode = StatusCodes.Status403Forbidden;
						await context.Response.WriteAsync("You do not have permission.");
					}
				}
				else
				{
					context.Response.StatusCode = StatusCodes.Status401Unauthorized;
					await context.Response.WriteAsync("Role not found in token.");
				}

			}
			catch (Exception ex)
			{
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
				await context.Response.WriteAsync($"Error validating token: {ex.Message}");
			}

		}



	}
}
