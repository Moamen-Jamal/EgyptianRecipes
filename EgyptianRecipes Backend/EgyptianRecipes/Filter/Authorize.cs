using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApi.Filter
{
    public class AUTHORIZE : AuthorizationFilterAttribute
    {
        public string Roles { get; set; }
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            string jwtToken =
                actionContext.Request.Headers.Authorization?.Parameter;
            if (string.IsNullOrEmpty(jwtToken))
            {
                actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.Unauthorized, "Not Authorized");
                return;
            }
            Dictionary<string, string> claims =
                SecurityHelper.Validate(jwtToken);
            if(claims == null)
            {
                actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.Unauthorized, "Not Authorized");
                return;
            }
            string roles = claims.First(i => i.Key == "Roles").Value;
            string[] userRoles = roles.Split(new char[] { ',' });
            string[] requiredRoles = Roles.Split(new char[] { ',' });
            if (!requiredRoles.Intersect(userRoles).Any())
            {
                actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.Unauthorized, "Not Authorized");
                return;
            }
        }
    }
}