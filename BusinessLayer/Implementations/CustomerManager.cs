using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Context;
using BusinessLayer.Interfaces;
using Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DbModels;
using Models.TransferObjects;

namespace BusinessLayer.Implementations
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerManager : ManagerBase, ICustomerManager
    {
        private readonly IEmailManager emailManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="emailManager"></param>
        /// <param name="logManager"></param>
        public CustomerManager(LokellaDbContext context, IEmailManager emailManager, ICacheManager cacheManager, ILogManager logManager) : base(context, cacheManager, logManager)
        {
            this.emailManager = emailManager;
        }

        public async Task<int> RegisterCustomerAndSendVerificationEmail(CustomerLogin customerLogin)
        {
            var customerId = 0;

            var existingCustomer = await Context.Customers
                .Where(q => q.Email == customerLogin.Email)
                .FirstOrDefaultAsync();

            if (existingCustomer == null)
            {
                var customer = await Context.Customers.AddAsync(new Customer
                {
                    Email = customerLogin.Email,
                    FirstName = customerLogin.FirstName,
                    LastName = customerLogin.LastName,
                    PhoneNumber = customerLogin.PhoneNumber
                });

                customerId = customer.Entity.Id;
            }
            else
            {
                existingCustomer.FirstName = customerLogin.FirstName;
                existingCustomer.LastName = customerLogin.LastName;
                existingCustomer.PhoneNumber = customerLogin.PhoneNumber;

                customerId = existingCustomer.Id;
            }

            await Context.SaveChangesAsync();

            emailManager.Send(customerLogin.Email, "Verify Email", "Lutfen eposta adresini su linke tiklayarak dogrulayiniz: "
                +
                "<a href='lokella://verify?code=" + customerLogin.DeviceId + "'>Dogrulama linki</a>");

            return customerId;
        }

        public async Task<int> DeleteCustomer(int customerId)
        {
            await Task.Delay(10);

            return 0;
        }
    }
}
