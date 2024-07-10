using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PRN221.ClinicDental.Business.DTO.Response.Dentist;
using PRN221.ClinicDental.Data.Common.Interface;
using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Services.Interfaces;

namespace PRN221.ClinicDental.Services
{
    public class DentistDetailService : IDentistDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DentistDetailService(IUnitOfWork unitOfWork,IMapper mapper )
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            
        }
        public async Task<List<DentistResponseModel>> GetDentistsByClinicId(int clinicId)
        {
            var listDentistDetail = await _unitOfWork.DentistDetailRepository.GetDentistsByClinicId(clinicId);
            return _mapper.Map<List<DentistResponseModel>>(listDentistDetail);
        }

        public async Task<List<DentistDetail>> GetDentistDetailsByClinicId(int clinicId)
        {
            var listDentistDetail = await _unitOfWork.DentistDetailRepository.GetDentistsByClinicId(clinicId);
            return listDentistDetail;
        }
    }
}
