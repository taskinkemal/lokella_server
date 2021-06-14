using Microsoft.AspNetCore.Mvc;
using WebCommon.BaseControllers;
using BusinessLayer.Interfaces;
using System.Collections.Generic;
using Models.DbModels;
using System.Net;
using Models.TransferObjects;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : AuthController
    {
        private readonly ICustomerManager customerManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerManager"></param>
        public CustomersController(ICustomerManager customerManager)
        {
            this.customerManager = customerManager;
        }

        [HttpGet]
        public List<int> Get()
        {
            var list = new List<int>();
            list.Add(4);
            list.Add(9);
            return list;
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<JsonResult> Post([FromBody] CustomerLogin customerLogin)
        {
            await customerManager.RegisterCustomerAndSendVerificationEmail(customerLogin);

            return ControllerHelper.CreateResponse(98);
        }
    }
}
