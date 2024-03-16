using Microsoft.EntityFrameworkCore;
using Vehicle_Maintenance_API.Context;

namespace Vehicle_Maintenance_API.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddPersistenceInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                ));

            #region Repositories
            //services.AddTransient(typeof(IRepositoryAsync<>));
            #endregion
        }
    }
}
