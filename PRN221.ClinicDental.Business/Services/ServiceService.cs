using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN221.ClinicDental.Business.Common.Interface;
using PRN221.ClinicDental.Business.DTO.Request.ServiceModel;
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
                ClinicServiceId = cs.ClinicServiceId,
                ServiceId = cs.Service.ServiceId,
                ServiceName = cs.Service.ServiceName,
                Description = cs.Service.Description,
                Price = cs.Price
            }).ToList();
            return serviceResponseModels;
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

        public async Task<ServiceViewAdminResponse> GetServiceByIdAsync(int id)
        {
            var service = await _unitOfWork.ServiceRepository.GetServiceById(id);
            if (service == null)
            {
                return null;
            }
            return _mapper.Map<ServiceViewAdminResponse>(service);

        }

        public async Task<List<ServiceViewAdminResponse>> GetAllServicesAdminView()
        {
            var service = await _unitOfWork.ServiceRepository.GetAllServices();
            return _mapper.Map<List<ServiceViewAdminResponse>>(service);
        }


        public async Task<ServiceViewAdminResponse> CreateServiceAsync(ServiceViewAdminRequest service)
        {
            var newService = new Service()
            {
                ServiceName = service.ServiceName,
                Description = service.Description,
                ImageURL = service.ImageURL,
            };
            await _unitOfWork.ServiceRepository.CreateAsync(newService);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<ServiceViewAdminResponse>(newService);
            

        }

        public async Task UpdateServiceAsync(ServiceViewAdminResponse service)
        {
            var serviceExist =await _unitOfWork.ServiceRepository.GetServiceById(service.ServiceId);
            if (serviceExist == null)
            {
                throw new Exception($"Not found serviceId: {service.ServiceId} ");

            }

            serviceExist.Description = service.Description;
            serviceExist.ImageURL = service.ImageURL;
            serviceExist.ServiceName= service.ServiceName;

            await _unitOfWork.ServiceRepository.UpdateAsync(serviceExist);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> DeleteServiceAsync(int serviceId)
        {
            var service = await _unitOfWork.ServiceRepository.GetServiceById(serviceId);
            if (service == null)
            {
                return false;
            }

            // Check if any clinic is associated with this service
            bool isServiceUsed = await _unitOfWork.ClinicServicesRepository.IsServiceUsedByAnyClinicAsync(serviceId);
            if (isServiceUsed)
            {
                throw new InvalidOperationException("Cannot delete service because it is associated with one or more clinics.");
            }

            // Proceed with deleting the service
            await _unitOfWork.ServiceRepository.DeleteAsync(service);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> ServiceNameExistsAsync(string serviceName)
        {
            return await _unitOfWork.ServiceRepository.ServiceNameExistsAsync(serviceName);
        }

        public async Task<List<Service>> GetAllListServicesForCreate()
        {
            return await _unitOfWork.ServiceRepository.GetAllServices();
        }

        public async Task<bool> UpdatePriceClinicServices(Data.Models.ClinicService clinicService)
        {
            var newClinicService = await _unitOfWork.ServiceRepository.GetServicesByClinicServiceId(clinicService.ClinicServiceId);
            newClinicService.Price = clinicService.Price;
            await _unitOfWork.ClinicServicesRepository.UpdateAsync(newClinicService);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> DeleteClinicServices(int clinicServiceId)
        {
            var clinicService = await _unitOfWork.ServiceRepository.GetServicesByClinicServiceId(clinicServiceId);
            await _unitOfWork.ClinicServicesRepository.DeleteAsync(clinicService);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> CreateClinicService(int serviceId, int clinicId, decimal price)
        {
            var clinicService = new Data.Models.ClinicService
            {
                ClinicId = clinicId,
                ServiceId = serviceId,
                Price = price
            };
            await _unitOfWork.ClinicServicesRepository.CreateAsync(clinicService);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<Data.Models.ClinicService> GetServiceByClinicServiceId(int id)
        {
            var clinicServices = await _unitOfWork.ServiceRepository.GetServicesByClinicServiceId(id);
            //    //var serviceResponseModels = new ServiceResponseModel
            //    //{
            //    //    ClinicServiceId = clinicServices.ClinicServiceId,
            //    //    ServiceId = clinicServices.Service.ServiceId,
            //    //    ServiceName = clinicServices.Service.ServiceName,
            //    //    Description = clinicServices.Service.Description,
            //    //    Price = clinicServices.Price
            //    //};
            return clinicServices;
        }

        public async Task<List<ServiceResponseModel>> GetServiceByClinicId(int clinicId)
        {
           var services = await _unitOfWork.ServiceRepository.GetServiceByClinicId(clinicId);
            return _mapper.Map<List<ServiceResponseModel>>(services);
        }
    }
}
