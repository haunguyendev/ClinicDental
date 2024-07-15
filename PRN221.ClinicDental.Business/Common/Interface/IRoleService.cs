using System.Collections.Generic;
using System.Threading.Tasks;
using PRN221.ClinicDental.Data.Models;

namespace PRN221.ClinicDental.Services.Interfaces
{
    public interface IRoleService
    {
        public Task<List<Role>> GetRolesAsync();
    }
}
