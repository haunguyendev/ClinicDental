using PRN221.ClinicDental.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Data.Common.Interface
{
    public interface IAppointmentRepository:IRepositoryBase<Appointment>
    {
        Task<List<Appointment>> GetListAppointmentByCustomerIdAsync(int customerId);
    }
}
