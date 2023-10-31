using AuthorizationService.BaseObjects;
using AuthorizationService.Data;
using AuthServices;
using Microsoft.IdentityModel.Tokens;
using SharedLib;
using SharedLib.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthorizationService.Service
{
    public class AuthenticationService
    {
        AccountService _accountService;
        JwtConfig jwtConfig;
        private IConfiguration _config;
        RefreshTokenDatas _tokenDatas;
        public AuthenticationService(IConfiguration config, 
            RefreshTokenDatas tokenDatas,
            AccountService accountService)
        {
            _config = config;
            _tokenDatas = tokenDatas;
            jwtConfig = _config.GetSection("Jwt").Get<JwtConfig>();
            _accountService= accountService;
        }
        public async Task<UserInfo> AuthenticateUser(LoginModel model)
        {
            UserInfo userInfo = null;
           BaseAppUser appUser=await  _accountService.GetUserInfo(model.UserName,model.CompanyID,model.AppID);
            if (appUser != null)
            {
                if ( appUser.Pwd==model.Password)
                {
                     userInfo = GetUserInfor(appUser);
                    return userInfo;
                }
            }
            return userInfo;
        }

        private UserInfo? GetUserInfor(BaseAppUser appUser)
        {
            throw new NotImplementedException();
        }

        public JwtData GenerateJSONWebToken(UserInfo userInfo)
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailAddress));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, userInfo.EmailAddress));
            //ID cua access token
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim("UserID", userInfo.ID.ToString()));
            claims.Add(new Claim("UserName", userInfo.UserName));
            claims.Add(new Claim("Roles", userInfo.Roles.ToString()));

           

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = credentials,
                Issuer = jwtConfig.Issuer,
                Audience = jwtConfig.Issuer,

            };
            JwtData data = new JwtData();
            

            var jwtoken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            string jwt = jwtSecurityTokenHandler.WriteToken(jwtoken);
            string refreshToken = GenerateRefreshToken();
        int ref_exp_mins= jwtConfig.RefExpireMinutes;

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
            throw new NotImplementedException();
        }

        public BODataProcessResult RenewToken(JwtData model)
        {
            BODataProcessResult processResult= new BODataProcessResult();   
            JwtSecurityTokenHandler tokenSecurityTokenHandler = new JwtSecurityTokenHandler();
            JwtConfig config = _config.GetSection("Jwt").Get<JwtConfig>();
            Byte[] seckeyBytes = Encoding.UTF8.GetBytes(config.Key);

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
                        processResult.Message= JwtStatus.InvalidToken.ToString();
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
        UserInfo GetUserInfoFromToken(string validToken)
        {
            UserInfo userInfo = new UserInfo();

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var jsonToken = jwtSecurityTokenHandler.ReadToken(validToken);
            var tokenS = jsonToken as JwtSecurityToken;

            userInfo.ID = tokenS.Claims.First(claim => claim.Type == "UserID").Value;
            userInfo.UserName = tokenS.Claims.First(claim => claim.Type == "UserName").Value;
            userInfo.Roles = tokenS.Claims.First(claim => claim.Type == "Roles").Value;
            userInfo.EmailAddress = tokenS.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Email).Value;
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
