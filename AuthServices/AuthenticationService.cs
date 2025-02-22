using AuthenticationDAL.DTO;
using AuthorizationService.BaseObjects;
using AuthServiceLibrary;
using AuthServices;
using AuthServices.Models;
using AuthServices.Models;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using MSASharedLib.DataTypes;
using MSASharedLib.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationService.Service
{
    public class AuthenticationService
    {
        AccountService _accountService;
        AppObjectService _appObjectService;
        AppUserService appUserService;
        private IConfiguration _config;
        AuthJwtUtil _jwtUtil;
        IMapper _mapper;
        LogLoginService _logLoginService;
        public AuthenticationService(IConfiguration config,
            AppUserService appUserService,
            AppObjectService appObjectService,
             AuthJwtUtil jwtUtil,
        AccountService accountService,
            IMapper mapper,
            LogLoginService logLoginService)
        {
            _config = config;
            _jwtUtil = jwtUtil;

            _accountService = accountService;
            _mapper = mapper;
            _appObjectService = appObjectService;
            this.appUserService = appUserService;
            _logLoginService = logLoginService;
        }
        public async Task<UserInfo> AuthenticateUser(LoginModel model)
        {
            UserInfo userInfo = null;
            BaseAppUser appUser = await _accountService.GetUserInfo(model.UserName, model.CompanyID, model.AppID);
            if (appUser != null)
            {
                string saltPass = model.Password + appUser.PwdKey;
                saltPass = MSASecurity.GetHash(saltPass);
                if (appUser.Pwd == saltPass)
                {
                    string errMessage = string.Empty;
                    bool result = false;
                    List<BaseAppObject> baseAppObjects = _appObjectService.GetDatas(out errMessage, out result);
                    if (result == true)
                    {
                        baseAppObjects = baseAppObjects.Where(x => x.AppID == model.AppID).ToList();
                    }


                    userInfo = new UserInfo();
                    userInfo.ID = appUser.ID.ToString();
                    userInfo.UserName = appUser.UserName;
                    userInfo.FullName = appUser.FullName;
                    userInfo.EmailAddress = appUser.Email;
                    if (appUser.Role != null)
                    {
                        userInfo.Roles.Add(appUser.Role.Number.ToLower());
                        if (appUser.Role.Rights != null && appUser.Role.Rights.Count > 0)
                        {
                            foreach (var item in appUser.Role.Rights)
                            {
                                var baseAppObject = baseAppObjects.FirstOrDefault(x => x.ID == item.AppObjectID);
                                if (baseAppObject != null && !userInfo.ObjectRights.ContainsKey(baseAppObject.MainFunction.ToLower()))
                                {
                                    List<string> objectRights = new List<string>();
                                    if (item.CanRead == true)
                                    {
                                        objectRights.Add("read");
                                    }
                                    if (item.CanCreate == true)
                                    {
                                        objectRights.Add("create");
                                    }
                                    if (item.CanUpdate == true)
                                    {
                                        objectRights.Add("update");
                                    }
                                    if (item.CanDelete == true)
                                    {
                                        objectRights.Add("delete");
                                    }
                                    if (objectRights.Count > 0)
                                    {
                                        userInfo.ObjectRights.Add(baseAppObject.MainFunction.ToLower(), objectRights);
                                    }
                                }
                            }
                        }
                    }

                    userInfo.CompanyID = appUser.Company.ID;
                    userInfo.AppID = model.AppID;
                    if (appUser.ManagerID.HasValue && appUser.ManagerID.Value > 0)
                    {
                        userInfo.ManagerID = appUser.ManagerID.Value;

                        BaseAppUser managerAppUser = appUserService.GetData(userInfo.ManagerID, out errMessage, out result);
                        if (managerAppUser != null)
                        {
                            if (!string.IsNullOrWhiteSpace(managerAppUser.Email))
                            {
                                userInfo.ManagerEmail = managerAppUser.Email;
                            }
                        }

                    }

                    if (appUser.BaseUserStores != null && appUser.BaseUserStores.Count > 0)
                    {
                        userInfo.StoreIDs = appUser.BaseUserStores.Select(x => x.StoreID).ToList();
                    }

                    return userInfo;
                }
            }
            return userInfo;
        }
        /// <summary>
        /// lop nay danh cho mobLogin
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BODataProcessResult> MobLogin(LoginModel model)
        {
            BODataProcessResult processResult = new BODataProcessResult();

            processResult.OK = false;
            processResult.Content = null;
            UserInfo user = await AuthenticateUser(model);
            if (user != null)
            {
                MobUserInfo mobUserInfo = _mapper.Map<MobUserInfo>(user);

                List<ObjectRight> objectRights = new List<ObjectRight>();
                foreach (var item in user.ObjectRights)
                {
                    ObjectRight objectRight = new ObjectRight
                    {
                        ObjectName = item.Key,
                        Rights = item.Value
                    };
                    objectRights.Add(objectRight);
                }
                mobUserInfo.ObjectRights = objectRights;
                mobUserInfo.LoginDate = DateTime.Now;
                // ==>log vao mongo DB thong tin sau
                // 
                // ID: dai dien cho 1 lan login= userID+deviceID
                // Status = Login/LogOut
                // ngay thuc hien
                string mode = _config.GetValue<string>(
                "AppConfig:ProductMode");
                if (mode.ToLower() != "dev")
                {
                    LogLoginUI logLoginUI = _mapper.Map<LogLoginUI>(user);
                    logLoginUI.ID = Guid.NewGuid();
                    logLoginUI.LoginDate = DateTime.Now;
                    BODataProcessResult logResult = await _logLoginService.Create(logLoginUI);
                    if (logResult.OK)
                    {
                        mobUserInfo.LoginID = logLoginUI.ID;
                    }
                }


                processResult.OK = true;
                processResult.Content = mobUserInfo;


            }
            return processResult;
        }
        public async Task<BODataProcessResult> Login(LoginModel model)
        {
            BODataProcessResult processResult = new BODataProcessResult();

            processResult.OK = false;
            processResult.Content = null;

            UserInfo user = await AuthenticateUser(model);

            if (user != null)
            {

                JwtData jwtData = _jwtUtil.GenerateJSONWebToken(user);
                LoginInfo loginInfo = new LoginInfo()
                {
                    LoginDate = DateTime.Now,
                    JwtData = jwtData
                };

                processResult.OK = true;
                processResult.Content = loginInfo;


            }
            return processResult;
        }


        public BODataProcessResult Logout(LogOutModel model)
        {
            return new BODataProcessResult() { OK = true };
        }
        public BODataProcessResult RenewToken(JwtData model)
        {
            return _jwtUtil.RenewToken(model);
        }

    }
}
