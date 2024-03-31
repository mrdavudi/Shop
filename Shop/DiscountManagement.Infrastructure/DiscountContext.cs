using DiscountManagement.Domain.ColleagueDiscountAgg;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace DiscountManagement.Infrastructure
{
    public class DiscountContext : DbContext
    {
        public DbSet<CustomerDiscount> CustomerDiscount { get; set; }
        public DbSet<ColleagueDiscount> ColleagueDiscount { get; set; }

        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(CustomerDiscountMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
