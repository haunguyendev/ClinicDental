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
        public async Task<UserProfileResponse> GetUserProfileAsync(int userId)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);
            if (user == null) throw new Exception("User not found");

            return _mapper.Map<UserProfileResponse>(user);
        }

        public async Task UpdateUserProfileAsync(UserProfileUpdateRequest request)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(request.UserId);
            if (user == null) throw new Exception("User not found");

            // Update user properties
            user.Name = request.Name;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.Address = request.Address;

            await _unitOfWork.UserRepository.UpdateUserAsync(user);
        }

        public async Task<bool> ChangeUserPasswordAsync(int userId, string currentPassword, string newPassword)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);
            if (user == null) throw new Exception("User not found");

            // Verify current password
            if (!_authentication.Verify(user.PasswordHash, currentPassword))
            {
                throw new Exception("Current password is incorrect");
            }

            // Hash new password and update
            user.PasswordHash = _authentication.Hash(newPassword);
            await _unitOfWork.UserRepository.UpdateUserAsync(user);

            return true;
        }
    }
}
