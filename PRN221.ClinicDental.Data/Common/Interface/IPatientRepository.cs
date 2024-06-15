using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Data.Common.Interface
{
    public interface IPatientRepository:IRepositoryBase<Patient>
    {
    }
}
