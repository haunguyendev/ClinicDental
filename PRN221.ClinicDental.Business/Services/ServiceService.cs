using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN221.ClinicDental.Business.Common.Interface;
using PRN221.ClinicDental.Business.DTO.Response.Dentist;
using PRN221.ClinicDental.Business.DTO.Response.ServiceResponse;
using PRN221.ClinicDental.Data.Common.Interface;
using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Data.Repositories;
using PRN221.ClinicDental.Services.Interfaces;

namespace PRN221.ClinicDental.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public ServiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            
        }
        public async Task<List<ServiceResponseModel>> GetAllListServices()
        {
            var listService = await _unitOfWork.ServiceRepository.GetAllServices();
            return _mapper.Map<List<ServiceResponseModel>>(listService);

        }

        public async Task<List<DentistResponseModel>> GetDentistsByServiceAndClinic(int serviceId, int clinicId)
        {
            var dentists = await _unitOfWork.ServiceRepository.GetDentistsByServiceAndClinic(serviceId, clinicId);
            return dentists.Select(d => new DentistResponseModel
            {
                DentistId = d.UserId,
                DentistName = d.User.Username
            }).ToList();
        }

        public async Task<ServiceResponseModel> GetServiceById(int serviceId)
        {
            var service = await _unitOfWork.ServiceRepository.GetServiceById(serviceId);
            return new ServiceResponseModel
            {
                ServiceId = service.ServiceId,
                ServiceName = service.ServiceName,
                Description = service.Description,
                
            };
        }

        public async Task<List<ServiceResponseModel>> GetServicesByClinicId(int clinicId)
        {
            var clinicServices = await _unitOfWork.ServiceRepository.GetServicesByClinicId(clinicId);
            var serviceResponseModels = clinicServices.Select(cs => new ServiceResponseModel
            {
                ServiceId = cs.Service.ServiceId,
                ServiceName = cs.Service.ServiceName,
                Description = cs.Service.Description,
                Price = cs.Price
            }).ToList();
            return serviceResponseModels;
        }
    }
}
