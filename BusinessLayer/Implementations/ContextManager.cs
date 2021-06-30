using BusinessLayer.Context;
using BusinessLayer.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace BusinessLayer.Implementations
{
    /// <summary>
    /// 
    /// </summary>
    public class ContextManager : IContextManager
    {

        /// <summary>
        /// 
        /// </summary>
        private IDbContextTransaction transaction;

        private LokellaDbContext context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logManager"></param>
        public ContextManager(LokellaDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void BeginTransaction()
        {
            transaction = context.Database.BeginTransaction();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Rollback()
        {
            if (transaction != null)
            {
                transaction.Rollback();
                transaction.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Commit()
        {
            if (transaction != null)
            {
                transaction.Commit();
                transaction.Dispose();
            }
        }
    }
}
