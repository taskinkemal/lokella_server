using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DbModels;
using WebCommon.BaseControllers;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpecialOffersController : NoAuthController
    {
        private readonly IBusinessManager businessManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessManager"></param>
        public SpecialOffersController(IBusinessManager businessManager)
        {
            this.businessManager = businessManager;
        }

        // <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/SpecialOffers/{id}")]
        public async Task<List<SpecialOffer>> Get(int id, bool isActive = true)
        {
            return await businessManager.GetSpecialOffers(id, isActive);
        }
    }
}
