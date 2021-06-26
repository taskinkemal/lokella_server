using System.Collections.Generic;
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
        public async Task<List<Business>> Get()
        {
            return await businessManager.GetBusinesses();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/Businesses/{id}/Info")]
        public async Task<BusinessInfo> GetBusinessInfo(int id)
        {
            return await businessManager.GetBusinessInfo(id);
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
