using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading;
using BusinessLayer.Interfaces;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WebCommon.Test")]
namespace WebCommon.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    public class ExecutionFilterAttribute : ActionFilterAttribute
    {
        private readonly IContextManager contextManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextManager"></param>
        public ExecutionFilterAttribute(IContextManager contextManager)
        {
            this.contextManager = contextManager;
        }

        /// <summary>
        /// Policy injection for all endpoints.
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            SetCulture(Thread.CurrentThread);

            contextManager.BeginTransaction();

            base.OnActionExecuting(context);

            contextManager.Commit();
        }

        internal static void SetCulture(Thread t)
        {
            t.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            t.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
        }
    }
}
