using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace SharedLib
{
    public class JwtUtil
    {
        public UserInfo GetUserInfo(string token)
        {
            UserInfo userInfo = new UserInfo();

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;

            // using JwtSecurityToken
            var fullname = tokenS.Claims.First(claim => claim.Type == "FullName").Value;

            userInfo.FullName = fullname;
            userInfo.EmailAddress = tokenS.Claims.First(claim => claim.Type == "EmailAddress").Value;
            userInfo.Roles = tokenS.Claims.First(claim => claim.Type == "Roles").Value;
            userInfo.UserName = tokenS.Claims.First(claim => claim.Type == "UserName").Value;
            userInfo.ID = tokenS.Claims.First(claim => claim.Type == "ID").Value;
            return userInfo;
        }

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
    }
}
