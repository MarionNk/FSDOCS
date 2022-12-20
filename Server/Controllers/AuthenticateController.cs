using FSDOCS.Server.Helpers;
using FSDOCS.Server.Models;
using FSDOCS.Server.Repositories.Contracts;
using FSDOCS.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace FSDOCS.Api.Controllers
{
    [Route("Authenticate")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly ITokenService tokenManager;

        public AuthenticateController(ITokenService tokenManager)
        {
            this.tokenManager = tokenManager;
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Authenticate([FromBody] AuthenticateRequest model)
        { 
            if (tokenManager.AuthenticateUser(model.Userid,model.Password))
            {
                var response = tokenManager.NewToken(model.Userid, model.Password);
                HttpContext.Session.SetString(SessionVariables.Token, response.Token);
                
                return Ok(response);
            }
            else
            {
                ModelState.AddModelError("Unauthorized", "You are not authorized");
                return Unauthorized(ModelState);
            }
        }
    }
}
