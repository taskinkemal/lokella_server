using Common;

namespace WebCommon.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBaseController
    {
        /// <summary>
        /// 
        /// </summary>
        int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        AuthenticationLevel AuthenticationLevel { get; set; }
    }
}
