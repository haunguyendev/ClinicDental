﻿using PRN221.ClinicDental.Data.Common.Interface;
using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Data.Repositories;
using PRN221.ClinicDental.Data.UnitOfWork;
using PRN221.ClinicDental.Services.Interfaces;
using PRN221.ClinicDental.Services;
using Microsoft.EntityFrameworkCore.Internal;
using PRN221.ClinicDental.Business.Common.Interface;

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
            return services;
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IClinicRepository, ClinicRepository>();
            services.AddScoped<IDoctorDetailRepository, DoctorDetailRepository>();
            services.AddScoped<ICertificateRepository, CertificateRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IDoctorServiceRepository, DoctorServiceRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        }
        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IClinicService, ClinicService>();
            services.AddScoped<IDoctorDetailService, DoctorDetailService>();
            services.AddScoped<ICertificateService, CertificateService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IDoctorServiceService, DoctorServiceService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
        }
    }
}
