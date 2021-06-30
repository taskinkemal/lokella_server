using System.Threading.Tasks;
using Common;
using Common.Interfaces;

namespace BusinessLayer.Interfaces
{
    public interface IAuthManager : IDependency
    {
        Task<(bool IsAuthenticated, int UserId, AuthenticationLevel Level)> VerifyAccessToken(string accessToken, int? businessId);
    }
}
