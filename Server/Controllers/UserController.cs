using FSDOCS.Server.Entities;
using FSDOCS.Server.Repositories;
using FSDOCS.Server.Repositories.Contracts;
using FSDOCS.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FSDOCS.Api.Controllers
{
    [Route("Users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userservcice;

        public UserController(IUserService  userservice)
        {
            this.userservcice = userservice;
        }
        [Route("GetUsers")]
        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            try
            {
                return Ok(await userservcice.GetUsers());
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("Create")]
        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] AuthenticateRequest user)
        {
            try
            {
                if (await userservcice.CreateUser(user.Userid, user.Password))
                {
                    return Ok();
                }
                return BadRequest("Echec de la création! l'utilisateur existe deja");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("GetUser/{userid}/{pwd}")]
        [HttpGet]
        public async Task<ActionResult> GetUser(string userid,string pwd)
        {
            try
            {
                return Ok(await userservcice.GetUser(userid,pwd));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("Delete/{userid}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteUser(string userid)
        {
            try
            {
                if ( await userservcice.DeleteUser(userid))
                {
                    return Ok();
                }
                return BadRequest("l'utilisateur n'existe pas");
            }
            catch (Exception)
            {
                throw;
            }
        }

        
    }
}
