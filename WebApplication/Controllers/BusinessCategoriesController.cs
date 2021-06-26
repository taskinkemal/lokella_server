using System.Collections.Generic;
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
    public class BusinessCategoriesController : NoAuthController
    {
        private readonly IBusinessManager businessManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessManager"></param>
        public BusinessCategoriesController(IBusinessManager businessManager)
        {
            this.businessManager = businessManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<BusinessCategory>> Get()
        {
            return await businessManager.GetBusinessCategories();
        }
    }
}
