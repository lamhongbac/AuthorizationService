using AuthorizationService.BaseObjects;
using AuthorizationService.Data;
using AuthServices;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SharedLib;
using SharedLib.Models;
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
        JwtConfig jwtConfig;
        private IConfiguration _config;
        RefreshTokenDatas _tokenDatas;
        IMapper _mapper;
        public AuthenticationService(IConfiguration config,
            AppObjectService appObjectService,
            RefreshTokenDatas tokenDatas,
            AccountService accountService,
            IMapper mapper)
        {
            _config = config;
            _tokenDatas = tokenDatas;
            jwtConfig = _config.GetSection("JwtConfig").Get<JwtConfig>();
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

        
        public JwtData GenerateJSONWebToken(UserInfo userInfo)
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            //email la field bat buoc
            if (!string.IsNullOrEmpty(userInfo.EmailAddress))
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailAddress));
               
            }
            //else
            //{
            //    throw new Exception("Email is not allow empty");
            //}
            

            if (userInfo.UserName != null)
            {
                claims.Add(new Claim("UserName", userInfo.UserName));
                claims.Add(new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName));
            }
            else
            {
                throw new Exception("UserName is not allow empty");
            }
                if (userInfo.ID != null)
                claims.Add(new Claim("UserID", userInfo.ID.ToString()));
            if (userInfo.FullName != null)
                claims.Add(new Claim("FullName", userInfo.FullName.ToString()));
            if (userInfo.AppID >0 )
                claims.Add(new Claim("AppID", userInfo.AppID.ToString()));
            if (userInfo.CompanyID > 0)
                claims.Add(new Claim("CompanyID", userInfo.CompanyID.ToString()));

            if (userInfo.Roles != null && userInfo.Roles.Count > 0)
            {
                foreach (var item in userInfo.Roles)
                {
                    claims.Add(new Claim("Roles", item));
                }
            }

            if (userInfo.ObjectRights!=null && userInfo.ObjectRights.Count > 0)
            {
                string objectRights = JsonConvert.SerializeObject(userInfo.ObjectRights);
                claims.Add(new Claim("ObjectRights", objectRights));
            }
            //    if (userInfo.ObjectRights.Count>0)
            //{
             
                
            //    foreach (string objectName in userInfo.ObjectRights.Keys)
            //    {
            //        List<string> rights = userInfo.ObjectRights[objectName];
            //        foreach (var right in rights)
            //        {
            //            claims.Add(new Claim(objectName, right));
            //        }
            //    }
            //}


            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(jwtConfig.ExpiredSeconds),
                SigningCredentials = credentials,
                Issuer = jwtConfig.Issuer,
                Audience = jwtConfig.Audience,

            };
            JwtData data = new JwtData();


            var jwtoken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            string jwt = jwtSecurityTokenHandler.WriteToken(jwtoken);
            string refreshToken = GenerateRefreshToken();
            int ref_exp_mins = jwtConfig.RefExpireMinutes;

            //save data to DB
            RefreshTokenData refreshTokenModel = new RefreshTokenData()
            {
                Id = Guid.NewGuid(),
                Token = refreshToken,
                IssuedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddMinutes(ref_exp_mins),


                IsRevoked = false,
                IsUsed = false,

                JwtId = jwtoken.Id,
                UserId = userInfo.ID
            };
            //save token
            _tokenDatas.AddToken(refreshTokenModel);

            data.AccessToken = jwt;
            data.RefreshToken = refreshTokenModel.Token;

            return data;
        }
        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);

            }
            return Convert.ToBase64String(random);
        }
        public BODataProcessResult Logout(LogOutModel model)
        {
            return new BODataProcessResult() { OK = true };
        }

        public BODataProcessResult RenewToken(JwtData model)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            JwtSecurityTokenHandler tokenSecurityTokenHandler = new JwtSecurityTokenHandler();
            JwtConfig config = _config.GetSection("Jwt").Get<JwtConfig>();
            Byte[] seckeyBytes = Encoding.UTF8.GetBytes(config.SecretKey);

            //b1. build token validate para
            var tokenValidPara = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(seckeyBytes),
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = false

            };

            try
            {
                //b2 check token is Valid
                var tokenValidation = tokenSecurityTokenHandler.ValidateToken(model.AccessToken, tokenValidPara
                    , out var validatedToken);
                if (validatedToken != null && validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                        StringComparison.InvariantCultureIgnoreCase);
                    if (!result)
                    {
                        processResult.OK = false;
                        processResult.Message = JwtStatus.InvalidToken.ToString();
                        return processResult;
                    }
                }
                //check expired
                var utcExpired = long.Parse(tokenValidation.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                var expireDate = ConvertUnixTimeToDate(utcExpired);
                if (expireDate > DateTime.UtcNow)
                {
                    processResult.OK = false;
                    processResult.Message = JwtStatus.TokenIsNotExpired.ToString();

                    return processResult;

                }
                //check reftoken is existed
                var storedToken = _tokenDatas.RefreshTokens.FirstOrDefault(x => x.Token == model.RefreshToken);
                if (storedToken == null)
                {
                    processResult.OK = false;
                    processResult.Message = JwtStatus.TokenIsNotExist.ToString();

                    return processResult;

                }
                //check token is used/revoked
                if (storedToken.IsUsed)
                {
                    processResult.OK = false;
                    processResult.Message = JwtStatus.TokenIsUsed.ToString();

                    return processResult;
                }
                if (storedToken.IsRevoked)
                {
                    processResult.OK = false;
                    processResult.Message = JwtStatus.IsRevoked.ToString();

                    return processResult;
                }
                //Check access token ID is correct
                var jti = tokenValidation.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value;

                if (storedToken.JwtId != jti)
                {
                    processResult.OK = false;
                    processResult.Message = JwtStatus.AccessTokenIdIsNotMatch.ToString();

                    return processResult;

                }

                //Update token
                storedToken.IsRevoked = true;
                storedToken.IsUsed = true;
                _tokenDatas.Update(storedToken);
                UserInfo userInfo = GetUserInfoFromToken(model.AccessToken);

                //su dung old token de lay lai cac thong tin cu
                // username

                JwtData token = GenerateJSONWebToken(userInfo);
                processResult.OK = false;
                processResult.Message = JwtStatus.Success.ToString();

                return processResult;

            }
            catch
            {
                processResult.OK = false;
                processResult.Message = JwtStatus.BadRequest.ToString();

                return processResult;
            }
            finally
            {

            }

        }
        /// <summary>
        /// chuyen doi jwt access ve User de su dung lai ham GenerateToken
        /// </summary>
        /// <param name="validToken"></param>
        /// <returns></returns>
        UserInfo GetUserInfoFromToken(string validToken)
        {
            UserInfo userInfo = new UserInfo();
            try
            {
                JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


                var jsonToken = jwtSecurityTokenHandler.ReadToken(validToken);
                var tokenS = jsonToken as JwtSecurityToken;

                userInfo.ID = tokenS.Claims.First(claim => claim.Type == "UserID").Value;
                userInfo.UserName = tokenS.Claims.First(claim => claim.Type == "UserName").Value;
                userInfo.EmailAddress = tokenS.Claims.First(claim => claim.Type == "EmailAddress").Value;
                userInfo.FullName = tokenS.Claims.First(claim => claim.Type == "FullName").Value;
                userInfo.AppID = Convert.ToInt32(tokenS.Claims.First(claim => claim.Type == "AppID").Value);
                userInfo.CompanyID = Convert.ToInt32(tokenS.Claims.First(claim => claim.Type == "CompanyID").Value);

                var roleClaims = tokenS.Claims.Where(x => x.Type == "Roles").ToList();

                foreach (var item in roleClaims)
                {
                    userInfo.Roles.Add(item.Value);
                }
                //userInfo.Roles = tokenS.Claims.First(claim => claim.Type == "Roles").Value;
                string strObjectRights = tokenS.Claims.First(claim => claim.Type == "ObjectNames").Value;

             Dictionary<string, List<string>> ObjectRights = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(strObjectRights);
                userInfo.ObjectRights = ObjectRights;


            }
            catch (Exception ex)
            {
                string error = ex.Message;
                userInfo = null;
                return userInfo;
            }
            return userInfo;
        }

        private DateTime ConvertUnixTimeToDate(long utcExpired)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpired).ToUniversalTime();
            return dateTimeInterval;
        }
    }
}
