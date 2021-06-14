using System.Net;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DbModels;
using WebCommon.BaseControllers;

namespace WebApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class BusinessesController : NoAuthController
    {
        private readonly IBusinessManager businessManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessManager"></param>
        public BusinessesController(IBusinessManager businessManager)
        {
            this.businessManager = businessManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Business Get()
        {
            return new Business
            {
                Id = 5
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/Businesses/{qrCode}")]
        public async Task<Business> GetFromQrCode(string qrCode)
        {
            return await businessManager.GetBusinessByQrCode(qrCode);
        }

        [HttpPut]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<JsonResult> Put([FromBody] Models.TransferObjects.Business business)
        {
            var result = await businessManager.InsertBusinessAsync(business);

            return ControllerHelper.CreateResponse(result);
        }
    }
}
