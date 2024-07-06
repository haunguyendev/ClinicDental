using Microsoft.EntityFrameworkCore;
using PRN221.ClinicDental.Data.Common.Interface;
using PRN221.ClinicDental.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Data.Repositories
{
    public class AppointmentRepository : RepositoryBase<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(ClinicDentalDbContext context) : base(context)
        {
        }

        public async Task<bool> CancelAppointmentAsync(int appointmentId,string status)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment == null)
                return false;

            appointment.Status = status;
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
            return true;

        }

        public bool CustomerHasAppointment(int customerId, int clinicId, DateTime appointmentDate, int slot)
        {
            return _context.Appointments
            .Any(a => a.CustomerId == customerId
                   && a.ClinicId == clinicId
                   && a.AppointmentTime.Date == appointmentDate.Date
                   && a.Slot == slot);
        }

        public int GetAppointmentsCountForSlot(int clinicId, int dentistId, DateTime appointmentDate, int slot)
        {
            return _context.Appointments
                .Count(a => a.ClinicId == clinicId && a.DentistId == dentistId && a.AppointmentTime.Date == appointmentDate.Date && a.Slot == slot);
        }

        public async Task<Appointment?> GetById(int id)
        {
            return await _context.Appointments.Include(x=>x.Dentist)
                .Include(x=>x.Clinic)
                .ThenInclude(x=>x.Address)
                .Include(x=>x.Service)
                .FirstOrDefaultAsync(x => x.AppointmentId == id);
        }

        public async Task<List<Appointment>> GetListAppointmentByCustomerIdAsync(int customerId)
        {
            return await _context.Appointments
          .Where(a => a.CustomerId == customerId)
          .Include(a => a.Dentist)
          .Include(a => a.Clinic)
          .ThenInclude(x => x.Address)
          .Include(a => a.Service)
          
          .ToListAsync();
        }

        
    }
}
