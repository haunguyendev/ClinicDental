using PRN221.ClinicDental.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Data.Common.Interface
{
    public interface IUserRepository:IRepositoryBase<User>
    {
        Task<User> GetUserByUserAndPassword(string user, string password);

        Task<User> FindByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAllUserAsync();
        Task<IEnumerable<User>> SearchUsersAsync(string searchString);

    }
}
