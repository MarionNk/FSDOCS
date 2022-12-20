using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FSDOCS.Server.Repositories.Contracts;
using System.Net;
using Microsoft.Extensions.FileProviders;
using FSDOCS.Server.Repositories;
using FSDOCS.Server.Entities;

namespace FSDOCS.Api.Controllers
{
    [Route("Upload")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService uploadService;

        public UploadController(IUploadService uploadService)
        {
            this.uploadService = uploadService;
        }

        [Route("GetFUbyDate/{dateupload}")]
        [HttpGet]
        public async Task<IActionResult> GetFailedtoUploadPreinsByDate(DateTime dateupload)
        {
            try
            {
                return Ok(await uploadService.FailedtoUploadPreinsByDate(dateupload));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetFailedtoUploadPreins()
        {
            try
            {
                return Ok(await uploadService.FailedtoUploadPreins());
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        [HttpPost]
        public async Task<ActionResult> UploadPreinscriptions(IFormFile file)
        {
           try
            {
                if (file.Length <= 0 )
                    return BadRequest("empty file");

                else if (!isXlsx(file))
                    return BadRequest($"extension is not .xlsx");

                else
                {
                    await uploadService.UploadPreinscriptions(file);
                    return Ok();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("All")]
        [HttpPost]
        public async Task<ActionResult> UploadAll(IFormFile file)
        {
           try
            {
                if (file.Length <= 0 )
                {
                    return BadRequest("empty file");
                }
                else if (!isXlsx(file))
                {
                    return BadRequest($"extension is not .xlsx");
                }
                else
                {
                    await uploadService.UploadPreinscriptions(file);
                    await uploadService.UploadEtudiants(file);
                    await uploadService.UploadEtapes(file);
                    await uploadService.UploadAA(file);
                    await uploadService.UploadDossiers(file);

                    return Ok();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        public async Task<ActionResult> UploadEtudiants(IFormFile file)
        {
           try
            {
                if (file.Length <= 0 )
                {
                    return BadRequest("empty file");
                }
                else if (!isXlsx(file))
                {
                    return BadRequest($"extension is not .xlsx");
                }
                else
                {
                    await uploadService.UploadEtudiants(file);
                    return Ok();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> UploadDossiers(IFormFile file)
        {
           try
            {
                if (file.Length <= 0 )
                {
                    return BadRequest("empty file");
                }
                else if (!isXlsx(file))
                {
                    return BadRequest($"extension is not .xlsx");
                }
                else
                {
                    await uploadService.UploadDossiers(file);
                    return Ok();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> UploadEtapes(IFormFile file)
        {
           try
            {
                if (file.Length <= 0 )
                {
                    return BadRequest("empty file");
                }
                else if (!isXlsx(file))
                {
                    return BadRequest($"extension is not .xlsx");
                }
                else
                {
                    await uploadService.UploadEtapes(file);
                    return Ok();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> UploadAA(IFormFile file)
        {
           try
            {
                if (file.Length <= 0 )
                {
                    return BadRequest("empty file");
                }
                else if (!isXlsx(file))
                {
                    return BadRequest($"extension is not .xlsx");
                }
                else
                {
                    await uploadService.UploadAA(file);
                    return Ok();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool isXlsx(IFormFile file)
        {
            if (file.FileName.Split(".")[1].Equals("xlsx"))
                return true;
            else
                return false;
        }

    }
}
