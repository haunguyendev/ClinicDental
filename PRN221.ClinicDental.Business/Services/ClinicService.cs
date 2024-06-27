using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using PRN221.ClinicDental.Business.DTO.Request.ClinicReqModel;
using PRN221.ClinicDental.Business.DTO.Response.Clinic;
using PRN221.ClinicDental.Business.Services;
using PRN221.ClinicDental.Data.Common.Interface;
using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Data.Repositories;
using PRN221.ClinicDental.Services.Interfaces;

namespace PRN221.ClinicDental.Services
{
    public class ClinicService : IClinicService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private readonly ICloudStorage _cloudStorage;
        public ClinicService(IUnitOfWork unitOfWork,IMapper mapper, ICloudStorage cloudStorage)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cloudStorage = cloudStorage;
        }
        public async Task<List<ClinicResponseModel>> GetClinicsByServiceId(int serviceId)
        {
            var listClinic = await _unitOfWork.ClinicRepository.GetClinicsByOnServiceId(serviceId);

            return listClinic.Select(c => new ClinicResponseModel
            {
                ClinicId = c.ClinicId,
                ClinicName = c.Name
            }).ToList();

        }
        public async Task<List<ClinicResponseModel>> GetAllClinic()
        {
            var listClinic = await _unitOfWork.ClinicRepository.GetAllClinics();
            return _mapper.Map<List<ClinicResponseModel>>(listClinic);
        }

        public async Task<List<ClinicResponseModel>> SearchClinicByName(string keyword)
        {
            var clinics = await _unitOfWork.ClinicRepository.SearchClinics(c => c.Name.Contains(keyword));
            return clinics.Select(c => new ClinicResponseModel
            {
                ClinicId = c.ClinicId,
                ClinicName = c.Name,
                StreetAddress = c.Address.StreetAddress,
                District = c.Address.District
            }).ToList();
        }

        public async Task<List<DistrictGroupModel>> GetClinicsGroupedByDistrict()
        {
            var clinics = await _unitOfWork.ClinicRepository.GetAllClinics();
            return clinics.GroupBy(c => c.Address.District)
                          .Select(g => new DistrictGroupModel
                          {
                              District = g.Key,
                              ClinicCount = g.Count()
                          }).ToList();
        }

        public async Task<List<Clinic>> GetClinicsByClinicOwnerId(int userId)
        {
            var clinics = await _unitOfWork.ClinicRepository.GetAllClinics();
            var listClinic = clinics.Where(x=> x.ClinicOwnerId == userId).ToList();
            return listClinic;
        }

        public async Task<bool> AddClinic(ClinicReqModel model, int customerId)
        {
            var filePath = "Clinincs";
            var imageUri = await _cloudStorage.UploadFile(model.ImageURL, filePath);
            var address = new Address()
            {
                StreetAddress = model.StreetAddress,
                District = model.District,
            };

            var clinic = new Clinic()
            {
                ClinicOwnerId = customerId,
                Name = model.Name,
                AddressId = address.AddressId,
                Address = address,
                ImageURL = imageUri
            };
            await _unitOfWork.ClinicRepository.AddNewClinic(clinic, model.ClinicServices) ;
            
            var listClinicServices = new List<PRN221.ClinicDental.Data.Models.ClinicService>();
            foreach(var service in model.ServiceId)
            {
                listClinicServices.Add(new PRN221.ClinicDental.Data.Models.ClinicService
                {
                   ClinicId = clinic.ClinicId,
                   ServiceId = service,
                   Price = 0
                });
            }
            await _unitOfWork.ClinicServicesRepository.CreateRange(listClinicServices);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
