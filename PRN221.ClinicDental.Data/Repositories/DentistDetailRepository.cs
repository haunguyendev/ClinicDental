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
    public class DentistDetailRepository : RepositoryBase<DentistDetail>, IDentistDetailRepository
    {
        public DentistDetailRepository(ClinicDentalDbContext context) : base(context)
        {
           
        }

        public async Task<List<DentistDetail>> GetDentistsByClinicId(int clinicId)
        {
            return await _context.DentistDetails.Where(x => x.ClinicId==clinicId).Include(x=>x.User).ToListAsync();
        }
    }
}
