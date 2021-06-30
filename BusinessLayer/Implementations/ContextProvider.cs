using BusinessLayer.Context;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Implementations
{
    public class ContextProvider : IContextProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ContextProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public LokellaDbContext GetContext()
        {
            return (LokellaDbContext)httpContextAccessor.HttpContext.RequestServices.GetService(typeof(LokellaDbContext));
        }
    }
}
