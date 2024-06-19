using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN221.ClinicDental.Business.DTO.Response;
using PRN221.ClinicDental.Data.Common.Interface;
using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Services.Interfaces;

namespace PRN221.ClinicDental.Services
{
    public class UserService : IUserService

    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

            
        }

        public async Task<UserLoginResponse> Authenticate(string username, string password)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUserAndPassword(username, password);
            return _mapper.Map<UserLoginResponse>(user);       
        }
    }
}
