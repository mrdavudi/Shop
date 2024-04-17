using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure
{
    public class AccountContext : DbContext
    {
        public DbSet<Account> Account { get; set; }
        public DbSet<Role> Role { get; set; }

        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(AccountMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
