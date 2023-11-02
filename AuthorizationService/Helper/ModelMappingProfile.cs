using AuthenticationDAL.DTO;
using AuthorizationService.BaseObjects;
using AuthorizationService.Data;
using AutoMapper;

namespace AuthorizationService.Helper
{
    public class ModelMappingProfile : Profile
    {
        public ModelMappingProfile() : base()
        {
            CreateMap<BaseCompany, CompanyUI>().ReverseMap();
            CreateMap<BaseAppObject, AppObjectUI>().ReverseMap();
            CreateMap<BaseRoleRight, RoleRightUI>().ReverseMap();
            CreateMap<BaseAppRole, AppRoleUI>().ReverseMap();
            CreateMap<BaseAppUser, AppUserUI>().ReverseMap();
            CreateMap<BaseApplication, ApplicationUI>().ReverseMap();
            CreateMap<BaseCompanyApplication, CompanyApplicationUI>().ReverseMap();
            CreateMap<BaseUserRole, UserRoleUI>().ReverseMap();
            CreateMap<BaseUserStore, UserStoreUI>().ReverseMap();
            CreateMap<AppUserData, BaseAppUser>().ReverseMap();
            CreateMap<BaseAppUser, UserInfo>().ReverseMap();
        }
    }
}
