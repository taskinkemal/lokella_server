﻿using Common;
using Microsoft.AspNetCore.Mvc;
using WebCommon.Attributes;

namespace WebCommon.BaseControllers
{
    /// <inheritdoc />
    /// <summary>
    /// Sets token to null when it's valid.
    /// </summary>
    [TypeFilter(typeof(ExecutionFilterAttribute))]
    public class NoAuthController : BaseController { }
}