using App.Application.IServices;
using App.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Application.Extentions
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServicesRegistration(this IServiceCollection services, IConfiguration configuration)
        {

            //Services
            services.AddSingleton(typeof(LocalizationService));
            services.AddTransient<IidentityService, IdentityService>();
            services.AddTransient<IClinicService, ClinicService>();
            services.AddTransient<IPatientService, PatientService>();
   



            return services;
        }
    }
}
