using AuthorizationService.BaseObjects;
using AuthorizationService.Data;
using AuthServices;

using AuthServices.Models;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SharedLib;
using SharedLib.Authentication;
using SharedLib.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthorizationService.Service
{
    public class AuthenticationService
    {
        AccountService _accountService;
        AppObjectService _appObjectService;
        
        private IConfiguration _config;
       JwtUtil _jwtUtil;
        IMapper _mapper;
        public AuthenticationService(IConfiguration config,
            AppObjectService appObjectService,
             JwtUtil jwtUtil,
        AccountService accountService,
            IMapper mapper)
        {
            _config = config;
            _jwtUtil = jwtUtil;
          
            _accountService = accountService;
            _mapper = mapper;
            _appObjectService = appObjectService;
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
                    if(result == true)
                    {
                        baseAppObjects = baseAppObjects.Where(x => x.AppID ==  model.AppID).ToList();
                    }


                    userInfo = new UserInfo();
                    userInfo.ID = appUser.ID.ToString();
                    userInfo.UserName = appUser.UserName;
                    userInfo.FullName = appUser.FullName;
                    userInfo.EmailAddress = appUser.Email;
                    if(appUser.Role != null)
                    {
                        userInfo.Roles.Add(appUser.Role.Number.ToLower());
                        if (appUser.Role.Rights != null && appUser.Role.Rights.Count > 0)
                        {
                            foreach(var item in appUser.Role.Rights)
                            {
                                var baseAppObject = baseAppObjects.FirstOrDefault(x => x.ID == item.AppObjectID);
                                if (baseAppObject != null && !userInfo.ObjectRights.ContainsKey(baseAppObject.MainFunction.ToLower()))
                                {
                                    List<string> objectRights = new List<string>();
                                    if(item.CanRead == true)
                                    {
                                        objectRights.Add("read");
                                    }
                                    if(item.CanCreate == true)
                                    {
                                        objectRights.Add("create");
                                    }
                                    if(item.CanUpdate == true)
                                    {
                                        objectRights.Add("update");
                                    }
                                    if(item.CanDelete == true)
                                    {
                                        objectRights.Add("delete");
                                    }
                                    if(objectRights.Count > 0)
                                    {
                                        userInfo.ObjectRights.Add(baseAppObject.MainFunction.ToLower(), objectRights);
                                    }
                                }
                            }
                        }
                    }
                    
                    userInfo.CompanyID = appUser.Company.ID;
                    userInfo.AppID = model.AppID;
                    return userInfo;
                }
            }
            return userInfo;
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
