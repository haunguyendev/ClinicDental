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
           return await _context.Users.Include(u=>u.Role).FirstOrDefaultAsync(x=>x.Username.Equals(username));
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            return await _context.Users.Include(u => u.Role).ToListAsync();
        }

        public Task<User> GetUserByUserAndPassword(string user, string password)
        {
            throw new NotImplementedException();
        }
        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<User>> SearchUsersAsync(string searchString)
        {
            if (string.IsNullOrEmpty(searchString)) searchString = "";
            return await _context.Users.Include(x=>x.Role)
               .Where(u => u.Username.Contains(searchString) || u.Name.Contains(searchString) || u.Email.Contains(searchString)||u.Role.RoleName.Contains(searchString))
               .ToListAsync();
        }

        public async Task<User> GetDentistById(int id)
        {
            return await _context.Users
                .Include(x => x.DentistDetail)
                .Include(x => x.DentistDetail.DentistServices).FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
    