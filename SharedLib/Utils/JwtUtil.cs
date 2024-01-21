using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AuthServices.Models;
using System.Security.Cryptography;
using SharedLib;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Net.Mail;
using SharedLib.Utils;

namespace AuthServices.Models
{
    /// <summary>
    /// lop nay phu thuoc 
    /// Kieu du lieu cua Authentication Service
    /// 
    /// </summary>
    public class AuthJwtUtil
    {
        public UserInfo GetUserInfoFromToken(string validToken)
        {
            JwtClientUtil jwtClientUtil = new JwtClientUtil();
            var claims = jwtClientUtil.GetClaims(validToken);

            try
            {
                UserInfo userInfo = new UserInfo();
                if (claims.FirstOrDefault(claim => claim.Type == "AppID") != null)
                {
                    int AppID = Convert.ToInt32(claims.First(claim => claim.Type == "AppID").Value);
                    userInfo.AppID = AppID;
                }
                if (claims.FirstOrDefault(claim => claim.Type == "CompanyID") != null)
                {
                    int CompanyID = Convert.ToInt32(claims.First(claim => claim.Type == "CompanyID").Value);
                    userInfo.CompanyID = CompanyID;
                }

                if (claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Email) != null)
                {
                    string EmailAddress = claims.First(claim => claim.Type == JwtRegisteredClaimNames.Email).Value;
                    userInfo.EmailAddress = EmailAddress;
                }
                if (claims.FirstOrDefault(claim => claim.Type == "FullName") != null)
                {
                    string FullName = claims.First(claim => claim.Type == "FullName").Value;
                    userInfo.FullName = FullName;
                }
                if (claims.FirstOrDefault(claim => claim.Type == "UserName") != null)
                {
                    string UserName = claims.First(claim => claim.Type == "UserName").Value;
                    userInfo.UserName = UserName;
                }
                if (claims.FirstOrDefault(claim => claim.Type == "UserID") != null)
                {
                    string ID = claims.First(claim => claim.Type == "UserID").Value;
                    userInfo.ID = ID;
                }





                var roleClaims = claims.Where(x => x.Type.ToLower() == "roles").ToList();

                foreach (var item in roleClaims)
                {
                    userInfo.Roles.Add(item.Value);
                }
                string strObjectRights = claims.First(claim => claim.Type.ToLower() == "objectrights").Value;

                Dictionary<string, List<string>> ObjectRights = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(strObjectRights);
                userInfo.ObjectRights = ObjectRights;

                return userInfo;

                //JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey));
                //var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


                //var jsonToken = jwtSecurityTokenHandler.ReadToken(validToken);
                //var tokenS = jsonToken as JwtSecurityToken;



                //userInfo.Roles = tokenS.Claims.First(claim => claim.Type == "Roles").Value;



            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }

        }
        private RefreshTokenDatas _tokenDatas;
        JwtConfig jwtConfig;
        public AuthJwtUtil(RefreshTokenDatas tokenDatas, JwtConfig jwtConfig)
        {
            _tokenDatas = tokenDatas;
            this.jwtConfig = jwtConfig;
        }
        public AuthJwtUtil()
        {

        }
        public void SetConfig(JwtConfig jwtConfig )
        {
            this.jwtConfig = jwtConfig;
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
            if (userInfo.AppID > 0)
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

            if (userInfo.ObjectRights != null && userInfo.ObjectRights.Count > 0)
            {
                string objectRights = JsonConvert.SerializeObject(userInfo.ObjectRights);
                claims.Add(new Claim("ObjectRights", objectRights));
            }



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
        //public UserInfo GetUserInfo(string token)
        //{
        //    UserInfo userInfo = new UserInfo();

        //    var handler = new JwtSecurityTokenHandler();
        //    var jsonToken = handler.ReadToken(token);
        //    var tokenS = jsonToken as JwtSecurityToken;

        //    // using JwtSecurityToken
        //    var fullname = tokenS.Claims.First(claim => claim.Type == "FullName").Value;

        //    userInfo.FullName = fullname;
        //    userInfo.EmailAddress = tokenS.Claims.First(claim => claim.Type == "EmailAddress").Value;
        //    var role = tokenS.Claims.First(claim => claim.Type == "Roles").Value;
        //    if (!string.IsNullOrWhiteSpace(role))
        //    {
        //        userInfo.Roles.Add(role);
        //    }
        //    userInfo.UserName = tokenS.Claims.First(claim => claim.Type == "UserName").Value;
        //    userInfo.ID = tokenS.Claims.First(claim => claim.Type == "ID").Value;
        //    return userInfo;
        //}

        /// <summary>
        /// kiem tra token is Valid truoc khi goi protect API,neu expired thi xin JWT moi
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool IsValid(string token)
        {
            JwtSecurityToken jwtSecurityToken;
            try
            {
                jwtSecurityToken = new JwtSecurityToken(token);
            }
            catch (Exception)
            {
                return false;
            }

            return jwtSecurityToken.ValidTo > DateTime.UtcNow;
        }
        /// <summary>
        /// nhan vao 1 token va pars
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public JwtSecurityToken? ParseToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;

            // using JwtSecurityToken
            //var jti = tokenS.Claims.First(claim => claim.Type == "jti").Value;
            return tokenS;
        }
        //var token = "[encoded jwt]";
        //var handler = new JwtSecurityTokenHandler();
        //var jwtSecurityToken = handler.ReadJwtToken(token);


        /// <summary>
        /// Renew Token base on valid token
        /// b1 build token validate para
        /// b2 check A token is Valid
        /// b3 check A token is  expired
        /// b4 check R token is existed in DB
        /// b5 check R token is NOT used/revoked
        /// b6 Check A token ID in R token is correct
        /// b7 Update token before using
        /// b8 Sinh ra UserInfo tren co so para token
        /// ======> Sinh ra new token <=======
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BODataProcessResult RenewToken(JwtData model)
        {
            JwtClientUtil jwtClientUtil=new JwtClientUtil();
            BODataProcessResult processResult = new BODataProcessResult();
            JwtSecurityTokenHandler tokenSecurityTokenHandler = new JwtSecurityTokenHandler();
            
            Byte[] seckeyBytes = Encoding.UTF8.GetBytes(jwtConfig.SecretKey);

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
                //b2 check A token is Valid
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
                //b3 check A token is  expired
                var utcExpired = long.Parse(tokenValidation.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                var expireDate = jwtClientUtil.ConvertUnixTimeToDate(utcExpired);
                if (expireDate > DateTime.UtcNow)
                {
                    processResult.OK = false;
                    processResult.Message = JwtStatus.TokenIsNotExpired.ToString();

                    return processResult;

                }
                //b4 check R token is existed in DB
                RefreshTokenData? storedRefToken = _tokenDatas.GetRefreshToken(model.RefreshToken);
                if (storedRefToken == null)
                {
                    processResult.OK = false;
                    processResult.Message = JwtStatus.TokenIsNotExist.ToString();

                    return processResult;

                }
                //b5 check R token is NOT used/revoked
                if (storedRefToken.IsUsed)
                {
                    processResult.OK = false;
                    processResult.Message = JwtStatus.TokenIsUsed.ToString();

                    return processResult;
                }
                if (storedRefToken.IsRevoked)
                {
                    processResult.OK = false;
                    processResult.Message = JwtStatus.IsRevoked.ToString();

                    return processResult;
                }
                //b6 Check access A tokenID in R token is correct
                var jti = tokenValidation.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                if (storedRefToken.JwtId != jti)
                {
                    processResult.OK = false;
                    processResult.Message = JwtStatus.AccessTokenIdIsNotMatch.ToString();

                    return processResult;

                }

                //b7 Update token before using
                storedRefToken.IsRevoked = true;
                storedRefToken.IsUsed = true;
                _tokenDatas.Update(storedRefToken);

                //b8 Sinh ra UserInfo tren co so para token
                UserInfo userInfo = GetUserInfoFromToken(model.AccessToken);

                //===> Sinh ra new token               
                if (userInfo != null)
                {
                    JwtData token = GenerateJSONWebToken(userInfo);

                    //<====== end process=======
                    processResult.Content = token;
                    processResult.OK = true;
                    processResult.Message = JwtStatus.Success.ToString();
                }
                else
                {
                    processResult.Content = null;
                    processResult.OK = false;
                    processResult.Message = JwtStatus.BadRequest.ToString();
                }
                return processResult;

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                processResult.OK = false;
                processResult.Message = JwtStatus.BadRequest.ToString();

                return processResult;
            }
            finally
            {

            }

        }
        


        
    }

    
}
