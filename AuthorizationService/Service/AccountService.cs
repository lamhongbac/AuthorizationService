//using AuthenticationDAL;
//using AuthenticationDAL.DTO;
//using AuthorizationService.BaseObjects;
//using AutoMapper;
//using MSASharedLib.DataTypes;
//namespace AuthorizationService.Service
//{
//    public class AccountService
//    {
//        AppUserDataPortal _appUserDataPortal;
//        IMapper _mapper;
//        private IConfiguration _config;
//        string _connectionString = "";
//        public AccountService(IMapper mapper,
//            IConfiguration config)
//        {
//            _mapper = mapper;
//            _config = config;
//            _connectionString = _config.GetSection("DBConfiguration:ConnectionString").Value;
//        }

//        public async Task<BaseAppUser> GetUserInfo(string userName, int companyID, int appID)
//        {
//            _appUserDataPortal = new AppUserDataPortal(_connectionString);
//            AppUserData appUserData = await _appUserDataPortal.GetAppUserData(userName, companyID, appID);
//            if (appUserData == null)
//            {
//                return null;
//            }
//            else
//            {
//                BaseAppUser baseAppUser = _mapper.Map<BaseAppUser>(appUserData.AppUser);
//                baseAppUser.Company = _mapper.Map<BaseCompany>(appUserData.Company);
//                baseAppUser.Role = _mapper.Map<BaseAppRole>(appUserData.AppRole);
//                baseAppUser.Role.Rights = _mapper.Map<List<BaseRoleRight>>(appUserData.RoleRights);
//                return baseAppUser;
//            }
//        }
//    }
//}
