using App.Domain.IReposetories;
using App.Infrastructure.Data;
using App.Infrastructure.Reposetories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure.Extentions
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServicesRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("AppConnectionString"));

            });
            //Repositories
            services.AddTransient(typeof(IBaseRepository<>), (typeof(BaseRepository<>)));
            services.AddTransient<IClinicRepository, CompanyRepository>();
    

            return services;
        }
    }
}
