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

        public async Task<List<Appointment>> GetListAppointmentByCustomerIdAsync(int customerId)
        {
            return await _context.Appointments.Where(x => x.CustomerId == customerId).ToListAsync();
    }
}
