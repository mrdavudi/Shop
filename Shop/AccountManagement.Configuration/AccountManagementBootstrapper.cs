using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Infrastructure;
using AccountManagement.Infrastructure.Repository;
using DomainManagement.Application;
using DomainManagement.Application.Contract.Account;
using DomainManagement.Application.Contract.Role;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountManagement.Configuration
{
    public class AccountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IAccountApplication, AccountApplication>();
            services.AddTransient<IAccountRepository, AccountRepository>();

            services.AddTransient<IRoleApplication, RoleApplication>();
            services.AddTransient<IRoleRepository, RoleRepository>();

            services.AddDbContext<AccountContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
