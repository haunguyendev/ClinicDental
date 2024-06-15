using PRN221.ClinicDental.Data.Common.Interface;
using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Data.Repositories;
using PRN221.ClinicDental.Data.UnitOfWork;

namespace PRN221.ClinicDental.Presentation.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ClinicDentalDbContext>();
            
            // Register Repositories
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IClinicRepository, ClinicRepository>();
            services.AddScoped<IDoctorDetailRepository, DoctorDetailRepository>();
            services.AddScoped<ICertificateRepository, CertificateRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IDoctorServiceRepository, DoctorServiceRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();

            // Register UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
