﻿using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Data.Common.Interface
{
    public interface IAppointmentRepository: IRepositoryBase<Appointment>
    {
        Task<Appointment?> GetById(int id);
        Task<List<Appointment>> GetListAppointmentByCustomerIdAsync(int customerId);
        int GetAppointmentsCountForSlot(int clinicId, int dentistId, DateTime appointmentDate, int slot);
        bool CustomerHasAppointment(int customerId, int clinicId, DateTime appointmentDate, int slot);
        Task<bool> CancelAppointmentAsync(int appointmentId,string status);

    }
}
