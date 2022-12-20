using FSDOCS.Server.Repositories;
using FSDOCS.Server.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FSDOCS.Api.Controllers
{
    [Route("Parametres")]
    [ApiController]
    public class DossierController : ControllerBase
    {
        private readonly IDossierService dossierService;

        public DossierController(IDossierService dossierService)
        {
            this.dossierService = dossierService;
        }
        [Route("getSF/{idpreins}")]
        [HttpGet]
        public async Task<IActionResult> getStudentFiles(int idpreins)
        {
            try
            {
                return Ok(await dossierService.getStudentFiles(idpreins));
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Route("GetAllFolderCodes")]
        [HttpGet]
        public async Task<IActionResult> GetAllFolderCodes()
        {
            try
            {
                return Ok(await dossierService.getDossier());
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("getAP")]
        [HttpGet]
        public async Task<IActionResult> GetAccessPath()
        {
            try
            {
                return Ok(await dossierService.GetAccessPath());
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("setAP")]
        [HttpPost]
        public async Task<ActionResult> setAccessPath(string path)
        {
            try
            {
                if (Path.IsPathRooted(path) && !Path.HasExtension(path) && Directory.Exists(path))
                {
                    await dossierService.setAccessPath(path);
                    return Ok();
                }

                else
                    return BadRequest("merci d'entrer un chemin absolue vers un dossier existant");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
