using PRN221.ClinicDental.Data.Common.Interface;
using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Data.Repositories;
using PRN221.ClinicDental.Data.UnitOfWork;
using PRN221.ClinicDental.Services.Interfaces;
using PRN221.ClinicDental.Services;
using Microsoft.EntityFrameworkCore.Internal;
using PRN221.ClinicDental.Business.Common.Interface;


using PRN221.ClinicDental.Business.MapperApplication;
using PRN221.ClinicDental.Business.Services;

namespace PRN221.ClinicDental.Presentation.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ClinicDentalDbContext>();

            
            
            // Register Repositories
          
            RegisterRepositories(services);

            //Register services
            RegisterServices(services);

            
            // Register UnitOfWorks
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(MapperProfile));
            return services;
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IClinicRepository, ClinicRepository>();
            services.AddScoped<IDentistDetailRepository, DentistDetailRepository>();
            
            
            services.AddScoped<IServiceRepository, ServiceRepository>();
          
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        }
        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IClinicService, Services.ClinicService>();
            services.AddScoped<IDentistDetailService, DentistDetailService>();
            services.AddScoped<ICertificateService, CertificateService>();
           
            services.AddScoped<IServiceService, ServiceService>();
            
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IAuthentication, Authentication>();
        }
    }
}
