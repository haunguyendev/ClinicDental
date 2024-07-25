using PRN221.ClinicDental.Data.Common.Interface;
using PRN221.ClinicDental.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Data.Repositories
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(ClinicDentalDbContext context) : base(context)
        {

        }

        public async Task<List<Role>> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
