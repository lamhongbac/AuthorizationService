using AuthenticationDAL.DTO;
using AuthorizationService.BaseObjects;
using AutoMapper;

namespace AuthorizationService.Helper
{
    public class ModelMappingProfile : Profile
    {
        public ModelMappingProfile() : base()
        {
            CreateMap<BaseCompany, CompanyUI>().ReverseMap();
            CreateMap<BaseAppObject, AppObjectUI>().ReverseMap();
            CreateMap<BaseAppRole, AppRoleUI>().ReverseMap();
            CreateMap<BaseAppUser, AppUserUI>().ReverseMap();
        }
    }
}
