using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN221.ClinicDental.Business.DTO.Response.Clinic;
using PRN221.ClinicDental.Data.Common.Interface;
using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Services.Interfaces;

namespace PRN221.ClinicDental.Services
{
    public class ClinicService : IClinicService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public ClinicService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            
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
    }
}
