using Microsoft.AspNetCore.Mvc;
using WebCommon.BaseControllers;
using BusinessLayer.Interfaces;
using System.Collections.Generic;
using Models.DbModels;
using System.Net;
using Models.TransferObjects;
using System.Threading.Tasks;
using WebCommon.Attributes;

namespace WebApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UsersController : AuthController
    {
        private readonly IUserManager userManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        public UsersController(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [Authenticate(Level = Common.AuthenticationLevel.NoAuthentication)]
        public async Task<JsonResult> Post([FromBody] TokenRequest tokenRequest)
        {
            var token = await userManager.Login(tokenRequest);

            return ControllerHelper.CreateResponse(token);
        }

        [HttpGet]
        public async Task<User> Get()
        {
            return await userManager.GetUser(UserId);
        }
    }
}
