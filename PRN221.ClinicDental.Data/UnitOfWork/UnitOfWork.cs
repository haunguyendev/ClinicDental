using PRN221.ClinicDental.Data.Common.Interface;
using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Data.Repositories;
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
            IDentistDetailRepository doctorDetailRepository,
            IDentistServiceRepository dentistServiceRepository,
            IServiceRepository serviceRepository,
            IAppointmentRepository appointmentRepository,
            IClinicServicesRepository clinicServicesRepository)
        {
            _dbContext = dbContext;
            RoleRepository = roleRepository;
            UserRepository = userRepository;
            ClinicRepository = clinicRepository;
            DentistDetailRepository = doctorDetailRepository;
            DentistServiceRepository = dentistServiceRepository;
            ServiceRepository = serviceRepository;
            AppointmentRepository = appointmentRepository;
            ClinicServicesRepository = clinicServicesRepository;
        }

        public IRoleRepository RoleRepository { get; }
        public IUserRepository UserRepository { get; }
        public IClinicRepository ClinicRepository { get; }
        public IDentistServiceRepository DentistServiceRepository { get; }
        public IClinicServicesRepository ClinicServicesRepository { get; }
        public IServiceRepository ServiceRepository { get; }

        public IAppointmentRepository AppointmentRepository { get; }

        public IDentistDetailRepository DentistDetailRepository { get; }

        public IAddressRepository AddressRepository => throw new NotImplementedException();

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
