﻿using System.Collections.Generic;
using System.Threading.Tasks;
using PRN221.ClinicDental.Business.DTO.Request;
using PRN221.ClinicDental.Business.DTO.Response;
using PRN221.ClinicDental.Business.DTO.Response.User;
using PRN221.ClinicDental.Business.Helper;
using PRN221.ClinicDental.Data.Models;

namespace PRN221.ClinicDental.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserProfileResponse> GetUserProfileAsync(int userId);
        Task<UserLoginResponse> Authenticate(string username, string password);
        Task RegisterUserAsync(UserRegisterRequest request);
        Task<bool> UpdateUserProfileAsync(UserProfileUpdateRequest request);
        Task<bool> ChangeUserPasswordAsync(int userId, string currentPassword, string newPassword);
        Task<PaginatedList<UserResponseModel>> GetPaginatedUsersAsync(int pageNumber, int pageSize);
        Task<PaginatedList<UserResponseModel>> SearchUsersAsync(string searchString, int pageNumber, int pageSize);
        Task<int> GetTotalPatientsAsync();
        Task<int> GetTotalClinicOwnersAsync();
        Task<int> GetTotalDentistsAsync();
    }
}
