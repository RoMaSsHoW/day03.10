
namespace ClassWork.Middlewares
{
    public class AuthorizationFixMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Headers.TryGetValue("Authorization", out var token))
            {
                if (!token.ToString().StartsWith("Bearer "))
                {
                    context.Request.Headers["Authorization"] = "Bearer " + token.ToString();
                }
            }

            await next(context);
        }
    }
}
