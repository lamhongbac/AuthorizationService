using AuthenticationDAL;
using AuthenticationDAL.DTO;
using AuthorizationService.BaseObjects;
using AuthorizationService.DataTypes;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthServices
{
    public class AccountService
    {
        AppUserDataPortal _appUserDataPortal;
        IMapper mapper;
        private IConfiguration _config;
        string connectionString = "";
        public AccountService(IMapper mapper,
            IConfiguration configuration)
        {
            var configSection = configuration.GetSection("AppConfig");
            AppConfiguration appConfig = configSection.Get<AppConfiguration>();
            this.connectionString = configuration.GetConnectionString(appConfig.ProductMode);
            this.mapper = mapper;
        }

        public async Task<BaseAppUser> GetUserInfo(string userName, int companyID, int appID)
        {
            _appUserDataPortal = new AppUserDataPortal(connectionString);
            AppUserData appUserData = await _appUserDataPortal.GetAppUserData(userName, companyID, appID);
            if (appUserData == null)
            {
                return null;
            }
            else
            {
                BaseAppUser baseAppUser = mapper.Map<BaseAppUser>(appUserData.AppUser);
                baseAppUser.Company = mapper.Map<BaseCompany>(appUserData.Company);
                baseAppUser.Role = mapper.Map<BaseAppRole>(appUserData.AppRole);
                baseAppUser.Role.Rights = mapper.Map<List<BaseRoleRight>>(appUserData.RoleRights);
                return baseAppUser;
            }
        }
    }
}
