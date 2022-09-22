using CarSharingBooking.CustomSetup;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CarSharingBooking.CustomStartup
{
    public class CustomAuthorizeFilter : AuthorizeFilter
    {
        public CustomAuthorizeFilter(AuthorizationPolicy policy) : base(policy){}

        public override async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            //throw exception by default
            if (context == null)
            {
                throw new ArgumentException(nameof(context));
            }

            //by pass the anoymous filter when the method is applied.
            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                return;
            }
            if (context.HttpContext.GetEndpoint().Metadata.GetMetadata<IAllowAnonymous>() != null)
            {
                return;
            }

            bool IsAuthorized = false;

            if (context.HttpContext.Request.Headers.ContainsKey("Username"))
            {
                string UsernameToken = context.HttpContext.Request.Headers["UserName"].ToString();
                IsAuthorized = !string.IsNullOrEmpty(UsernameToken);
            }

            if (!IsAuthorized)
            {
                context.Result = new ObjectResult(new
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Message = "Unauthorized access is detected"
                });
            }
        }
    }
}
