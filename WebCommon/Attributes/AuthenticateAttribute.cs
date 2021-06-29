using System;
using Common;

namespace WebCommon.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthenticateAttribute : Attribute
    {
        public AuthenticationLevel Level { get; set; } = AuthenticationLevel.SystemAdmin;
    }
}
