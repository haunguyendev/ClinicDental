using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Core;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using PRN221.ClinicDental.Business.Common.Interface;
using PRN221.ClinicDental.Business.DTO.Request;
using PRN221.ClinicDental.Business.DTO.Request.Dentist;
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
                RoleId = 4 
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

        public async Task<bool> UpdateUserProfileAsync(UserProfileUpdateRequest request)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(request.UserId);
            if (user == null) throw new Exception("User not found");

            user.Name = request.Name;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.Address = request.Address;

            await _unitOfWork.UserRepository.UpdateUserAsync(user);
            await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task<bool> ChangeUserPasswordAsync(int userId, string currentPassword, string newPassword)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);
            if (user == null) throw new Exception("User not found");

            if (!_authentication.Verify(user.PasswordHash, currentPassword))
            {
                throw new Exception("Current password is incorrect");
            }

            user.PasswordHash = _authentication.Hash(newPassword);
            await _unitOfWork.UserRepository.UpdateUserAsync(user);
            await _unitOfWork.CommitAsync();

            return true;
        }
        public async Task<int> GetTotalPatientsAsync()
        {
            var query = await _unitOfWork.UserRepository.GetAllUserAsync();
            return query.Count(u => u.RoleId == 4);
        }

        public async Task<int> GetTotalClinicOwnersAsync()
        {
            var query = await _unitOfWork.UserRepository.GetAllUserAsync();
            return query.Count(u => u.RoleId == 2);
        }

        public async Task<int> GetTotalDentistsAsync()
        {
            var query = await _unitOfWork.UserRepository.GetAllUserAsync();
            return query.Count(u => u.RoleId == 3);
        }

        public async Task UpdateDentist(DentistUpdateReqModel Dentist)
        {
            var chosenDentist = await _unitOfWork.UserRepository.GetDentistById(Dentist.ID);
            chosenDentist.Name = Dentist.FullName;
            chosenDentist.PhoneNumber = Dentist.PhoneNumber;
            chosenDentist.Address = Dentist.Address;
            chosenDentist.DentistDetail.Certificate = Dentist.Certificate;
            chosenDentist.DentistDetail.Qualification = Dentist.Qualification;
            chosenDentist.DentistDetail.Experience = Dentist.Experience;
            var listService = new List<DentistService>();

            foreach (var item in Dentist.ServiceId)
            {
                listService.Add(new DentistService
                {
                    DentistId = Dentist.ID,
                    ServiceId = item,
                });
            }
            var serviceList = new List<DentistService>();
            foreach (var item in Dentist.ServiceId)
            {
                serviceList.Add(new DentistService
                {
                    DentistId = Dentist.ID,
                    ServiceId = item,
                });
            }

            List<DentistService> existedList = chosenDentist.DentistDetail.DentistServices.ToList();

            var removeList = existedList.Where(t => !serviceList.Contains(t)).ToList();
            var addList = serviceList.Except(removeList).ToList();

            foreach (var item in removeList)
            {
                chosenDentist.DentistDetail.DentistServices.Remove(item);
            }
            foreach (var item in addList)
            {
                chosenDentist.DentistDetail.DentistServices.Add(item);
            }
            await _unitOfWork.UserRepository.UpdateAsync(chosenDentist);
            await _unitOfWork.CommitAsync();
        }

        public async Task<User> GetDentistById(int id)
        {
            return await _unitOfWork.UserRepository.GetDentistById(id);
        }

        public async Task<bool> CreateDentist(DentistReqModel dentist, int ClinicId)
        {


            var existingUser = await _unitOfWork.UserRepository.FindByUsernameAsync(dentist.Username);
            if (existingUser != null)

            {
                throw new Exception("Username already exists.");
            }

            // Create new User entity
            var newUser = new User
            {
                Username = dentist.Username,
                PasswordHash = _authentication.Hash(dentist.Password), // Hash the password
                Name = dentist.FullName,
                Email = dentist.Email,
                PhoneNumber = dentist.PhoneNumber,
                Address = dentist.Address,
                RoleId = 3, // Assuming 2 is the RoleID for 'Customer'
            };

            var listService = new List<DentistService>();

            foreach (var item in dentist.ServiceId)
            {
                listService.Add(new DentistService
                {
                    DentistId = newUser.UserId,
                    ServiceId = item,
                });
            }

            var DentistDetail = new DentistDetail()
            {
                ClinicId = ClinicId,
                Certificate = dentist.Certificate,
                Qualification = dentist.Qualification,
                Experience = dentist.Experience,
                DentistServices = listService,
            };
            newUser.DentistDetail = DentistDetail;
            await _unitOfWork.UserRepository.CreateAsync(newUser);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<User> GetUserByEmail(string Email)
        {
           return await _unitOfWork.UserRepository.FindByEmailAsync(Email);
        }

        public async Task<User> GetUserByUserName(string username)
        {
            return await _unitOfWork.UserRepository.FindByUsernameAsync(username);
        }
    }
}
