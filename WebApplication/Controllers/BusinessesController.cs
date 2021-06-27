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
        [Route("/Businesses/{qrCode}/Customer/{customerId}")]
        public async Task<Business> GetFromQrCode(string qrCode, int customerId)
        {
            var business = await businessManager.GetBusinessByQrCode(qrCode);

            if (business != null)
            {
                await businessManager.VisitBusiness(business.Id, customerId);
            }

            return business;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/Businesses/{id}/CustomerVisits")]
        public async Task<List<Models.TransferObjects.CustomerVisit>> GetCustomerVisits(int id)
        {
            return await businessManager.GetCustomerVisits(id);
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
