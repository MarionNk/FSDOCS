using Microsoft.AspNetCore.Mvc;
using FSDOCS.Server.Repositories.Contracts;
using FSDOCS.Server.Entities;
using FSDOCS.Server.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace FSDOCS.Api.Controllers
{
    [Route("Preins")]
    [ApiController]
    //[Authenticate]
    public class PreInscriptionController : ControllerBase
    {
        private readonly IPreinscriptionService preinscriptionservice;

        public PreInscriptionController(IPreinscriptionService preinscriptionservice)
        {
            this.preinscriptionservice = preinscriptionservice;
        }
        [Route("GetList")]
        [HttpGet]
        public async Task<ActionResult> GetListePreins()
        {
            try
            {
                var preins = await preinscriptionservice.GetPreInscriptions();
                if (preins == null)
                {
                    return NotFound();
                }
                return Ok(preins);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Route("GetByID/{idpreins}")]
        [HttpGet]
        public async Task<ActionResult> GetByID(int idpreins)
        {
            try
            {
                var preins = await preinscriptionservice.GetPreInscriptionsByID(idpreins);
                if (preins == null)
                {
                    return NotFound();
                }
                return Ok(preins);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        //Route("FilterListePreins")]
        public async Task<ActionResult> FilterListePreins(string? matricule, string? codeetape, string? codecycle, string? codeaa, string? etuname, string? etusurname)
        {
            try
            {
                var preins = await preinscriptionservice.FilterListePreins(matricule,codeetape,codecycle,codeaa, etuname, etusurname);
                if (preins == null)
                {
                    return NotFound();
                }
                return Ok(preins);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
    }
}
