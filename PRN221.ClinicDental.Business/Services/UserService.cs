using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Core;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using PRN221.ClinicDental.Business.Common.Interface;
using PRN221.ClinicDental.Business.DTO.Request;
using PRN221.ClinicDental.Business.DTO.Response;
using PRN221.ClinicDental.Business.DTO.Response.User;
using PRN221.ClinicDental.Business.Helper;
using PRN221.ClinicDental.Data.Common.Interface;
using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Data.Repositories;
using PRN221.ClinicDental.Services.Interfaces;

namespace PRN221.ClinicDental.Services
{
    public class UserService : IUserService

    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IAuthentication _authentication;
        public UserService(IUnitOfWork unitOfWork,IMapper mapper, IAuthentication authentication)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authentication = authentication;  
        }

        public async Task<UserLoginResponse> Authenticate(string username, string password)
        {
            var user = await _unitOfWork.UserRepository.FindByUsernameAsync(username);
            if(user == null)
            {
                return null;
            }
            else { 
            var check = _authentication.Verify(user.PasswordHash, password);
            if(check == false) { 
                return null;
            }
             return _mapper.Map<UserLoginResponse>(user);
            }
        }

        public async Task<PaginatedList<UserResponseModel>> GetPaginatedUsersAsync(int pageNumber, int pageSize)
        {
            var query =  await _unitOfWork.UserRepository.GetAllUserAsync();
            
            var listUserResponseModel = _mapper.Map<List<UserResponseModel>>(query);
            var listUserResponseModelQuery = listUserResponseModel.AsQueryable<UserResponseModel>(); 

            var users = PaginatedList<UserResponseModel>.Create(listUserResponseModel.AsQueryable(), pageNumber, pageSize);
            return users;
        }
        public async Task<PaginatedList<UserResponseModel>> SearchUsersAsync(string searchString, int pageNumber, int pageSize)
        {
            var query = await _unitOfWork.UserRepository.SearchUsersAsync(searchString);
            var userResponseModels = _mapper.Map<List<UserResponseModel>>(query);
            return PaginatedList<UserResponseModel>.Create(userResponseModels.AsQueryable(), pageNumber, pageSize);
        }

    

        public async Task RegisterUserAsync(UserRegisterRequest request)
        {
            var existingUser = await _unitOfWork.UserRepository.FindByUsernameAsync(request.Username);
            if (existingUser != null)

            {
                throw new Exception("Username already exists.");
            }

            // Create new User entity
            var newUser = new User
            {
                Username = request.Username,

                PasswordHash = _authentication.Hash(request.Password), // Hash the password
                Name = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                RoleId = 2 // Assuming 2 is the RoleID for 'Customer'
            };

            // Add the user to the repository
            await _unitOfWork.UserRepository.CreateAsync(newUser);

            // Commit the transaction
            await _unitOfWork.CommitAsync();
        }

       
    }
}
