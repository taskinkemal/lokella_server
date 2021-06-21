using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DbModels;
using WebCommon.BaseControllers;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : NoAuthController
    {
        private readonly IFilesManager filesManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filesManager"></param>
        public FilesController(IFilesManager filesManager)
        {
            this.filesManager = filesManager;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var file = filesManager.GetFile(id).Result;

            return File(file.FileContent, "image/png");
        }

        [HttpPut]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<JsonResult> Put([FromBody] Models.TransferObjects.File file)
        {
            var fileId = await filesManager.UploadFile(file);
            return ControllerHelper.CreateResponse(0);
        }
    }
}
