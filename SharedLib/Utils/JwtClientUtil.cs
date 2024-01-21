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
