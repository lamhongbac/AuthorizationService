using AuthenticationDAL;
using AuthenticationDAL.DTO;
using AuthorizationService.BaseObjects;
using AutoMapper;

namespace AuthorizationService.Service
{
    public class AccountService
    {
        AppUserDataPortal _appUserDataPortal;
        IMapper _mapper;
        private IConfiguration _config;
        string _connectionString = "";
        public AccountService(IMapper mapper,
            IConfiguration config)
        {
            _mapper = mapper;
            _config = config;
            _connectionString = _config.GetSection("DBConfiguration:ConnectionString").ToString();
        }

        public async Task<BaseAppUser> GetUserInfo(string userName, int companyID, int appID)
        {
            _appUserDataPortal = new AppUserDataPortal(_connectionString);
            AppUserData appUserData = await _appUserDataPortal.GetAppUserData(userName, companyID, appID);
            if (appUserData == null)
            {
                return null;
            }
            else
            {
                BaseAppUser baseAppUser = _mapper.Map<BaseAppUser>(appUserData);
                return baseAppUser;
            }
        }
    }
}
