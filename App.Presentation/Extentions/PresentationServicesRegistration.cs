using App.Application.Resources;
using App.Domain.Entities.Identity;
using App.Infrastructure.Data;
using App.Presentation.Helpers;
using App.Presentation.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using System.Reflection;

namespace App.Presentation.Extentions
{
    public static class PresentationServicesRegistration
    {
        public static IServiceCollection AddPresentationServicesRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(30);   // How long RememberMe lasts
                options.SlidingExpiration = true;                 // Refresh cookie on activity
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>
                (options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredLength = 3;
                    options.User.RequireUniqueEmail = false;
                }
                ).AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddAutoMapper(typeof(MappingProfile));
            services.AddSingleton(typeof(SideMenuItemsCollection));


            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                {
                    var assemblyName = new AssemblyName(typeof(SharedResources).GetTypeInfo().Assembly.FullName);
                    return factory.Create("SharedResources", assemblyName.Name);
                };

            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
        new CultureInfo("ar"),
        new CultureInfo("en"),
    };

                foreach (var culture in supportedCultures)
                {
                    culture.NumberFormat.NumberDecimalSeparator = ".";
                    culture.NumberFormat.NumberNegativePattern = 2;
                    culture.NumberFormat.NegativeSign = "-";
                }

                options.DefaultRequestCulture = new RequestCulture("ar");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });


            return services;
        }
    }
}
