using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Claims.Models
{
    public class ClaimsAuthorizeAttribute: AuthorizeAttribute
    {
        public string Claim { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var principal = filterContext.HttpContext.User as ClaimsPrincipal;

            if (!principal.HasClaim(c => c.Value == Claim))
            {
                filterContext.HttpContext.Response.StatusCode = 403;
                filterContext.Result = new ViewResult { ViewName = "Error" };
            }

            base.OnAuthorization(filterContext);
        }
    }
}