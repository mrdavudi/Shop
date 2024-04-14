using Comment.Application;
using Comment.Application.Contract;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure;
using CommentManagement.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CommentManagement.Configuration
{
    public class CommentManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<ICommentApplication, CommentApplication>();
            services.AddTransient<ICommentRepository, CommentRepository>();

            services.AddDbContext<CommentContext>(x => x.UseSqlServer(connectionString));

        }
    }
}
