using Microsoft.AspNetCore.Mvc;
using WebCommon.Attributes;
using WebCommon.Interfaces;
using Common;

namespace WebCommon.BaseControllers
{
    /// <summary>
    /// </summary>
    [Route("api/[controller]")]
    [TypeFilter(typeof(ExceptionHandlerAttribute))]
    public class BaseController : ControllerBase, IBaseController
    {
        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public AuthenticationLevel AuthenticationLevel { get; set; } = AuthenticationLevel.NoAuthentication;
    }
}
