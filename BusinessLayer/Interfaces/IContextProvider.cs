using BusinessLayer.Context;
using Common.Interfaces;

namespace BusinessLayer.Interfaces
{
    public interface IContextProvider : IDependency
    {
        LokellaDbContext GetContext();
    }
}
