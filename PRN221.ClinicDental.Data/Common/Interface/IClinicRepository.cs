using PRN221.ClinicDental.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Data.Common.Interface
{
    public interface IClinicRepository:IRepositoryBase<Clinic>
    {
        Task<List<Clinic>> GetAllClinics();
        Task<List<Clinic>> GetClinicsByOnServiceId(int serviceId);
        Task<List<Clinic>> SearchClinics(Expression<Func<Clinic, bool>> predicate);
        Task<bool> AddNewClinic(Clinic clinic, List<Service> service);
        Task<List<Clinic>> GetClinicsByDistrict(string district);
        Task<Clinic> GetClinicById(int? id);

    }
}
