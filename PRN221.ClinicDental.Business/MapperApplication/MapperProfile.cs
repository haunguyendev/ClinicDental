using AutoMapper;
using PRN221.ClinicDental.Business.DTO.Response;
using PRN221.ClinicDental.Business.DTO.Response.Dentist;
using PRN221.ClinicDental.Business.DTO.Response.ServiceResponse;
using PRN221.ClinicDental.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Business.MapperApplication
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserLoginResponse>()
                .ForMember(dest => dest.Role, src => src.MapFrom(x => x.Role.RoleName));
            CreateMap<Service, ServiceResponseModel>();
            CreateMap<DentistDetail, DentistResponseModel>()
                .ForMember(dest=>dest.DentistId,src=>src.MapFrom(x=>x.UserId))
                .ForMember(dest=>dest.DentistName,src=>src.MapFrom(x=>x.User.Name));
        }
    }
}
