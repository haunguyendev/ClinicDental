using System.Collections.Generic;
using System.Threading.Tasks;
using PRN221.ClinicDental.Business.DTO.Response;
using PRN221.ClinicDental.Data.Models;

namespace PRN221.ClinicDental.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserLoginResponse> Authenticate(string username, string password);
       
    }
}
