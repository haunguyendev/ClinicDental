using PRN221.ClinicDental.Business.DTO.Request;
using AutoMapper;
using PRN221.ClinicDental.Data.Models;
namespace PRN221.ClinicDental.Presentation.Pages.Accounts
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // This will automatically map properties with matching names and types
            CreateMap<User, UserProfileResponse>();
        }
    }
}
