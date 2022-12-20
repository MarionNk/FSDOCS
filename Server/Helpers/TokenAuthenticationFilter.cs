using FSDOCS.Server.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FSDOCS.Server.Helpers
{
    public class AuthenticateAttribute : TypeFilterAttribute
    {
        public AuthenticateAttribute() : base(typeof(TokenAuthenticationFilter)) { }
    }

    public class TokenAuthenticationFilter : Attribute, IAuthorizationFilter
    {
       
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //var tokenManager = (ITokenService) context.HttpContext.RequestServices.GetService(typeof(ITokenService));   
            
            string Apitoken = context.HttpContext.Request.Headers[SessionVariables.Token];

            string SessionToken = context.HttpContext.Session.GetString(SessionVariables.Token);

            if (String.IsNullOrEmpty(Apitoken) || String.IsNullOrEmpty(SessionToken))
            {
                unAuthorized(context);
            }
            else 
            if (SessionToken!=Apitoken)
            {
                unAuthorized(context);
            }
        }
        private void unAuthorized(AuthorizationFilterContext context)
        {
            context.ModelState.AddModelError("Unauthorized", "You are not authorized");
            context.Result = new UnauthorizedObjectResult(context.ModelState);
        }
    }
}
