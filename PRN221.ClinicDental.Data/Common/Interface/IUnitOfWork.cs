﻿using PRN221.ClinicDental.Data.Models;
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
        IDentistDetailRepository DentistDetailRepository { get; }

        IDentistServiceRepository DentistServiceRepository { get; }
        IAddressRepository AddressRepository { get; }
        IServiceRepository ServiceRepository { get; }
       
        IAppointmentRepository AppointmentRepository { get; }

        IClinicServicesRepository ClinicServicesRepository { get; }

        Task<int> CommitAsync();
    }
}
