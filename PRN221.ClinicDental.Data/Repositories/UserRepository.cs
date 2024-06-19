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
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ClinicDentalDbContext context) : base(context)
        {
        }

        public async Task<User> FindByUsernameAsync(string username)
        {
           return await _context.Users.FirstOrDefaultAsync(x=>x.Username.Equals(username));
        }

        public Task<User> GetUserByUserAndPassword(string user, string password)
        {
            throw new NotImplementedException();
        }
    }
}
