using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CarSharingBooking.CustomSetup
{
    public static class ExceptionLogging
    {
        public static void APIConfigureExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler( appError => {
                appError.Run(async context => {
                    context.Response.StatusCode  = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>().Error;
                    if (contextFeature != null)
                    {
                        var logger = loggerFactory.CreateLogger("Global Exception");
                        logger.LogError($"Something went wrong: Error : { contextFeature.InnerException } + " +
                            $"Stack Trace : { contextFeature.Message}");
                    }

                    await context.Response.WriteAsync(new LoggerDetails() { 
                        StatusCode = context.Response.StatusCode,
                        Message    = "There is something wrong, please check the log"
                    }.ToString());
                });
            });
        }
    }
}
