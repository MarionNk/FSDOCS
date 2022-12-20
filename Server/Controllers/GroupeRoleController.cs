using FSDOCS.Server.Entities;
using FSDOCS.Server.Repositories;
using FSDOCS.Server.Repositories.Contracts;
using FSDOCS.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;


namespace FSDOCS.Server.Controllers
{
    [Route("Groupes")]
    [ApiController]
    public class GroupeRoleController : ControllerBase
    {

        private readonly IGroupeService groupeservcice;

        public GroupeRoleController(IGroupeService groupeservice)
        {
            this.groupeservcice = groupeservice;
        }

        [Route("GetGroupesRole")]
        [HttpGet]
        public async Task<ActionResult> GetGroupesRole()
        {
            try
            {
                return Ok(await groupeservcice.GetGroupesRole());
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Route("GetGroupe")]
        [HttpGet]
        public async Task<ActionResult> GetGroupes()
        {
            try
            {
                return Ok(await groupeservcice.GetGroupes());
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("GetRoles")]
        [HttpGet]
        public async Task<ActionResult> GetRoles()
        {
            try
            {
                return Ok(await groupeservcice.GetRoles());
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("{codegroupe}")]
        public async Task<ActionResult> GetRolesByGroupe(string codegroupe)
        {
            try
            {
                return Ok(await groupeservcice.GetRolesByGroupe(codegroupe));
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Route("GetGBId/{codegroupe}")]
        [HttpGet]
        public async Task<ActionResult> GetGroupeById(string codegroupe)
        {
            try
            {
                return Ok(await groupeservcice.GetGroupeByID(codegroupe));
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Route("DeleteGR/{codegroupe}/{coderole}")]
        [HttpDelete]
        public async Task<ActionResult> removeRoleToGrp(string codegroupe,string coderole)
        {
            try
            {
                return Ok(await groupeservcice.removeRoleToGrp(codegroupe,coderole));
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Route("DeleteGroupe/{codegroupe}")]
        [HttpDelete]
        public async Task<ActionResult> removeGrp(string codegroupe)
        {
            try
            {
                return Ok(await groupeservcice.removeGrp(codegroupe));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("UpdateUser/{userid}")]
        [HttpPut]
        public async Task<ActionResult> ChangeUserGroup(string userid, [FromBody] UpdateUser updateUser)
        {
            try
            {
                if (await groupeservcice.ChangeUserGroup(userid, updateUser))
                {
                    return Ok();
                }
                return BadRequest("l'utilisateur ou le groupe n'existe pas");
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Route("UpdateGroupeRole/{codegroupe}")]
        [HttpPost]
        public async Task<ActionResult> UpdateGroupeRole(string codegroupe,[FromBody] List<string> rolesCode)
        {
            try
            {
                if (await groupeservcice.UpdateGroupeRole(codegroupe, rolesCode))
                {
                    return Ok();
                }
                return BadRequest("Assurez-vous que les roles et le groupe existent!");
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Route("CreateGroup")]
        [HttpPost]
        public async Task<ActionResult> CreateGroup([FromBody] GroupeCreate group)
        {
            try
            {
                await groupeservcice.CreateGroup(group);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Assurez-vous que les roles et le groupe existent!");
            }
        }

    }
}
