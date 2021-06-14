using Common;
using Microsoft.AspNetCore.Mvc;
using WebCommon.Attributes;

namespace WebCommon.BaseControllers
{
    /// <inheritdoc />
    /// <summary>
    /// Throws when token is invalid.
    /// </summary>
    [TypeFilter(typeof(ExecutionFilterAttribute))]
    public class AuthController : BaseController { }
}