using PRN221.ClinicDental.Data.Common.Interface;
using PRN221.ClinicDental.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ClinicDentalDbContext _dbContext;

        public UnitOfWork(
            ClinicDentalDbContext dbContext,
            IRoleRepository roleRepository,
            IUserRepository userRepository,
            IClinicRepository clinicRepository,
            IDoctorDetailRepository doctorDetailRepository,
            ICertificateRepository certificateRepository,
            IPatientRepository patientRepository,
            IServiceRepository serviceRepository,
            IDoctorServiceRepository doctorServiceRepository,
            IAppointmentRepository appointmentRepository)
        {
            _dbContext = dbContext;
            RoleRepository = roleRepository;
            UserRepository = userRepository;
            ClinicRepository = clinicRepository;
            DoctorDetailRepository = doctorDetailRepository;
            CertificateRepository = certificateRepository;
            PatientRepository = patientRepository;
            ServiceRepository = serviceRepository;
            DoctorServiceRepository = doctorServiceRepository;
            AppointmentRepository = appointmentRepository;
        }

        public IRoleRepository RoleRepository { get; }
        public IUserRepository UserRepository { get; }
        public IClinicRepository ClinicRepository { get; }
        public IDoctorDetailRepository DoctorDetailRepository { get; }
        public ICertificateRepository CertificateRepository { get; }
        public IPatientRepository PatientRepository { get; }
        public IServiceRepository ServiceRepository { get; }
        public IDoctorServiceRepository DoctorServiceRepository { get; }
        public IAppointmentRepository AppointmentRepository { get; }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
