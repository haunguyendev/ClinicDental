using PRN221.ClinicDental.Data.Common.Interface;
using PRN221.ClinicDental.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Data.Repositories
{
    public class DentistServiceRepository : RepositoryBase<DentistService>, IDentistServiceRepository
    {
        public DentistServiceRepository(ClinicDentalDbContext context) : base(context)
        {

        }
    }
}
