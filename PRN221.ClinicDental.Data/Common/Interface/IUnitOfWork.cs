using PRN221.ClinicDental.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Data.Common.Interface
{
    public interface IUnitOfWork: IDisposable
    {
        IRoleRepository RoleRepository { get; }
        IUserRepository UserRepository { get; }
        IClinicRepository ClinicRepository { get; }
        IDoctorDetailRepository DoctorDetailRepository { get; }
        ICertificateRepository CertificateRepository { get; }
        IPatientRepository PatientRepository { get; }
        IServiceRepository ServiceRepository { get; }
        IDoctorServiceRepository DoctorServiceRepository { get; }
        IAppointmentRepository AppointmentRepository { get; }

        Task<int> CommitAsync();
    }
}
