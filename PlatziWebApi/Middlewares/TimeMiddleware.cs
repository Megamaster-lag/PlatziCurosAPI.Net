using Microsoft.AspNetCore.Builder;

namespace PlatziWebApi.Middlewares
{
    public class TimeMiddleware
    {
        //Nos ayuda a llamar al siguiente middleware que sigue
        readonly RequestDelegate next;

        public TimeMiddleware(RequestDelegate nextRequest) 
        {
            next = nextRequest;   
        }

        public async Task Invoke(Microsoft.AspNetCore.Http.HttpContext context)
        {
            await next(context);

            if (context.Request.Query.Any(p => p.Key == "time"))
            {
                await context.Response.WriteAsync(DateTime.Now.ToShortDateString());
            }
        }
      
    }

    public static class TimeMiddlewareExtension
    {
        public static IApplicationBuilder UserTimeMiddleware(this IApplicationBuilder build)
        {
            return build.UseMiddleware<TimeMiddleware>();
        }
    }
}
