using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN221.ClinicDental.Business.Common.Interface;
using PRN221.ClinicDental.Business.DTO.Response.ServiceResponse;
using PRN221.ClinicDental.Data.Common.Interface;
using PRN221.ClinicDental.Data.Models;
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

        public async Task<ServiceResponseModel> GetServiceByIdAsync(int id)
        {
            var service= await _unitOfWork.ServiceRepository.GetServiceById(id);
            if (service == null)
            {
                throw new Exception("Service not found!");
            }
           return _mapper.Map<ServiceResponseModel>(service);

        }

        public async Task<List<Service>> GetServiceByListIdAsync(List<int> ids)
        {
            var service = await _unitOfWork.ServiceRepository.GetServiceByListId(ids);
            if (service == null)
            {
                throw new Exception("Service not found!");
            }
            return service;

        }
    }
}
