using Lift.Infra.Services.Logger;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Lift.App.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IMyLogger myLogger;

        public ExceptionHandlingMiddleware(RequestDelegate next, IMyLogger myLogger)
        {
            this.myLogger = myLogger;
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next.Invoke(context);
            }
            catch (Exception ex)
            {
                var error = this.myLogger.Log(ex, context, "");

                context.Response.StatusCode = error.StatusCode ?? (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync((string)error.Value);
            }
        }

    }
}
