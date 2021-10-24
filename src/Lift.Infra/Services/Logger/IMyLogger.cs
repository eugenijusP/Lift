using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Lift.Infra.Services.Logger
{
    public interface IMyLogger
    {

        ObjectResult Log(Exception ex, HttpContext context, string method);

        void LogSentryWarrning(Exception ex);
    }
}
