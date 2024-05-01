using SharedKernel.Abstractions;
using SharedKernel.FileUploader.Models;
using TramerQuery.Data.Abstractions;
using TramerQuery.Service.AppSettings;
using TramerQuery.Service.ServiceInterfaces;
using TramerQuery.Service.ServiceInterfaces.Interfaces;

namespace TramerQuery.Api.Infrastructure.ServiceCollectionExtensions
{
    public static class DependencyInjectionSetup
    {
        public static void AddDependencyInjectionSetup(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IFileWriter, FileWriter>();
            services.AddSingleton<IAppSettings, AppSettings>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ITramerQueryResultService, TramerQueryResultService>();
        }
    }
}
