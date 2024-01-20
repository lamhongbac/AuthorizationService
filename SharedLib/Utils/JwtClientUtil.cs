using AuthServices.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SharedLib.Utils
{
    public class JwtClientUtil
    {/// <summary>
     /// chuyen doi jwt access ve User de su dung lai ham GenerateToken
     /// </summary>
     /// <param name="validToken"></param>
     /// <returns></returns>
        public UserInfo GetUserInfoFromToken(string validToken)
        {
            var claims = GetClaims(validToken);

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

        public List<Claim> GetClaims(string accessToken)
        {
            //kiem tra token is valid truoc khi tra ve

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(accessToken);
            var tokenS = jsonToken as JwtSecurityToken;

            // using JwtSecurityToken
            //var fullname = tokenS.Claims.First(claim => claim.Type == "FullName").Value;
            return tokenS.Claims.ToList();
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
        /// kiem tra token su dung:JwtSecurityToken
        /// 
        /// rule 1:kiem tra expired Time
        /// parse token => get expired time compare with current
        /// </summary>
        /// <param name="AccessToken"></param>
        /// <returns></returns>
        public bool IsAccessTokenExpired(string AccessToken)
        {
            JwtSecurityToken jwtSecurityToken;
            try
            {

                jwtSecurityToken = new JwtSecurityToken(AccessToken);
                return jwtSecurityToken.ValidTo > DateTime.UtcNow;

            }
            catch (Exception)
            {
                return false;
            }


        }
        /// <summary>
        /// kiem tra token su dung
        /// JwtSecurityTokenHandler
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public bool IsTokenExpiredV1(string accessToken)
        {
            var claims = GetClaims(accessToken);
            var utcExpired = long.Parse(claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expireDate = ConvertUnixTimeToDate(utcExpired);
            return (expireDate > DateTime.UtcNow);
        }

        public DateTime ConvertUnixTimeToDate(long utcExpired)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpired).ToUniversalTime();
            return dateTimeInterval;
        }
    }
}
