using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading;
using BusinessLayer.Interfaces;
using System.Runtime.CompilerServices;
using Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Threading.Tasks;
using WebCommon.Interfaces;
using WebCommon.BaseControllers;
using System.Net;

[assembly: InternalsVisibleTo("WebCommon.Test")]
namespace WebCommon.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    public class ExecutionFilterAttribute : ActionFilterAttribute
    {
        private readonly IAuthManager authManager;
        private readonly IContextManager contextManager;
        private readonly AuthenticationLevel authenticationLevel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authManager"></param>
        /// <param name="contextManager"></param>
        /// <param name="authenticationLevel"></param>
        public ExecutionFilterAttribute(IAuthManager authManager, IContextManager contextManager, AuthenticationLevel authenticationLevel)
        {
            this.authManager = authManager;
            this.contextManager = contextManager;
            this.authenticationLevel = authenticationLevel;
        }

        /// <summary>
        /// Policy injection for all endpoints.
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            RetrieveParameters(context.HttpContext.Request.Headers, out var accessToken);
            SetCulture(Thread.CurrentThread);

            contextManager.BeginTransaction();

            var businessId = FindBusinessId(context);
            var result = ValidateRequest(context.Controller as IBaseController, accessToken, businessId).Result;

            if (ProceedWithExecution(result.level, authenticationLevel, HasAuthenticateAttribute(context.ActionDescriptor as ControllerActionDescriptor)))
            {
                base.OnActionExecuting(context);
                contextManager.Commit();
            }
            else
            {
                context.Result = ControllerHelper.CreateErrorResponse(HttpStatusCode.Unauthorized, result.errPhrase);
            }
        }

        internal async Task<(AuthenticationLevel level, string errPhrase)> ValidateRequest(IBaseController controller, string accessToken, int? businessId)
        {
            string errPhrase;
            AuthenticationLevel level = AuthenticationLevel.NoAuthentication;

            if (accessToken != null)
            {
                if (controller != null)
                {
                    var verificationResult = await authManager.VerifyAccessToken(accessToken, businessId);

                    if (verificationResult.IsAuthenticated)
                    {
                        controller.UserId = verificationResult.UserId;
                        controller.AuthenticationLevel = level = verificationResult.Level;
                    }
                    else
                    {
                        errPhrase = "InvalidToken";
                        return (level, errPhrase);
                    }
                }
                else
                {
                    errPhrase = "InvalidController";
                    return (level, errPhrase);
                }
            }
            else
            {
                errPhrase = "InvalidToken";
                return (level, errPhrase);
            }

            return (level, "");
        }

        internal static void RetrieveParameters(IHeaderDictionary headers, out string accessToken)
        {
            accessToken = GetHeader(headers, "Authorization", "");
            if (!string.IsNullOrWhiteSpace(accessToken) && accessToken.StartsWith("Bearer ", StringComparison.InvariantCulture))
            {
                accessToken = accessToken.Substring("Bearer ".Length);
            }
            else
            {
                accessToken = null;
            }
        }

        private static string GetHeader(IHeaderDictionary headers, string key, string defaultValue)
        {
            return headers.ContainsKey(key) ? Convertor.ToString(headers[key].First(), defaultValue) : defaultValue;
        }

        internal static void SetCulture(Thread t)
        {
            t.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            t.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
        }

        internal static bool ProceedWithExecution(
            AuthenticationLevel userAuthenticationLevel,
            AuthenticationLevel controllerAuthenticationLevel,
            AuthenticationLevel? methodAuthenticateAttribute)
        {
            var requiredLevel = methodAuthenticateAttribute ?? controllerAuthenticationLevel;
            return userAuthenticationLevel >= requiredLevel;
        }

        internal static AuthenticationLevel? HasAuthenticateAttribute(ControllerActionDescriptor descriptor)
        {
            var attr = descriptor?.MethodInfo?.GetCustomAttributes(true).OfType<AuthenticateAttribute>().FirstOrDefault();
            return attr?.Level;
        }

        internal static int? FindBusinessId(ActionExecutingContext context)
        {
            if (context.Controller is ManagementController && context.ActionArguments.ContainsKey("businessId"))
            {
                return (int)context.ActionArguments["businessId"];
            }

            return null;
        }
    }
}
