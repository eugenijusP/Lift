using Lift.Domain.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sentry;
using System;

namespace Lift.Infra.Services.Logger
{
    public class MyLogger : IMyLogger
    {
        private readonly ILogger<AppLogs> logger;

        public MyLogger(ILogger<AppLogs> logger) => this.logger = logger;

        /// <summary>
        /// Klaidos loginimas Sentry platformoje
        /// </summary>
        /// <param name="ex">Klaidos pranešimas</param>
        /// <param name="context">HTTP kvietimas</param>
        /// <param name="method">Kokiame metode įvyko klaida</param>
        /// <returns></returns>
        public ObjectResult Log(Exception ex, HttpContext context, string method)
        {
            var code = Utils.ExceptionResult(ex);
            var obj = new ObjectResult(ex.Message)
            {
                StatusCode = code
            };
            try
            {
                var fields = (IExceptionFields)ex;
                if (!fields.logSentry)
                {
                    return obj;
                }
            }
            catch
            {
                //If exception is not custom, logSentry property my not exits
            }

            try
            {
                var clientAdress = context.Connection?.RemoteIpAddress?.ToString() ?? string.Empty;

                SentrySdk.ConfigureScope(scope =>
                {
                    scope.SetTag("ip_address", clientAdress);
                    if (!string.IsNullOrEmpty(context.User.Identity?.Name))
                    {
                        scope.SetTag("username", context.User.Identity.Name);
                    }

                    scope.SetExtra("Method", method ?? string.Empty);
                });

                if (code == 500)
                {
                    this.logger.LogError(ex, ex.Message);
                }
                else
                {
                    this.logger.LogWarning(ex, ex.Message);
                }
            }
            catch (Exception ex2)
            {
                Utils.LogEventToFile("errror.txt", ex2.Message);
            }

            return obj;
        }

        public void LogSentryWarrning(Exception ex)
        {
            try
            {
                this.logger.LogError(ex, ex.Message);
            }
            catch (Exception ex2)
            {
                Utils.LogEventToFile("errror.txt", ex2.Message);
            }
        }
    }
}
