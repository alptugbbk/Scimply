using DbScimplyAPI.Application.Abstractions.Services;
using DbScimplyAPI.Application.Repositories;

namespace DbScimplyAPI.API
{
    public class AuthMiddleware
    {

        private readonly RequestDelegate _next;


        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;

        }


        public async Task InvokeAsync(HttpContext context, IServiceScopeFactory scopeFactory)
        {

            try
            {

                if (context.Request.Path.StartsWithSegments("/api/auth"))
                {
                    await _next(context);
                    return;
                }

                using (var scope = scopeFactory.CreateScope())
                {

                    var adminRepository = scope.ServiceProvider.GetRequiredService<IAdminRepository>();

                    var userId = context.Request.Headers["UserId"].ToString();

                    var user = adminRepository.GetAll(false).Where(x => x.Id == userId).FirstOrDefault();

                    if (user == null)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("User is not authorized.");
                        return;
                    }

                    await _next(context);

                }

            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
            }

        }


    }
}
