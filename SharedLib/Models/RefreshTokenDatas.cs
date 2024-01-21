using System;
using System.Collections.Generic;
using System.Linq;

namespace AuthServices.Models
{
    //public class RefreshTokenDatas
    //{
    //    public RefreshTokenDatas()
    //    {
    //        RefreshTokens = new List<RefreshTokenData>();
    //    }
    //    private static List<RefreshTokenData> RefreshTokens { get; set; }

    //    /// <summary>
    //    /// Add new token
    //    /// </summary>
    //    /// <param name="token"></param>
    //    public bool AddToken(RefreshTokenData newtoken)
    //    {
    //        if (RefreshTokens != null)
    //        {
    //            var exist = RefreshTokens.FirstOrDefault(x => x.Id == newtoken.Id);
    //            if (exist != null)
    //            {
    //                throw new Exception("new token is exist");
    //                //fasle
    //            }
    //            else
    //            {
    //                RefreshTokens.Add(newtoken);
    //                return true;
    //            }
    //        }
    //        else
    //        {
    //            RefreshTokens = new List<RefreshTokenData>
    //            {
    //                newtoken
    //            };
    //            return true;
    //        }

    //    }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="token"></param>
    //    public bool RemoveToken(RefreshTokenData oldtoken)
    //    {
    //        if (RefreshTokens != null)
    //        {
    //            var exist = RefreshTokens.FirstOrDefault(x => x.Id == oldtoken.Id);
    //            int index = RefreshTokens.IndexOf(exist);
    //            if (index != -1)
    //            {
    //                RefreshTokens.RemoveAt(index);
    //                return true;
    //            }
    //            else
    //            {
    //                return false;
    //            }

    //        }
    //        else
    //        {
    //            //RefreshTokens = new List<RefreshTokenModel>();

    //            throw new Exception("old token is not exist");
    //        }
    //    }

    //    public bool Update(RefreshTokenData storedToken)
    //    {
    //        if (RefreshTokens != null)
    //        {
    //            var exist = RefreshTokens.FirstOrDefault(x => x.Id == storedToken.Id);
    //            int index = RefreshTokens.IndexOf(exist);
    //            if (index != -1)
    //            {
    //                RefreshTokens[index] = storedToken;
    //                return true;
    //            }
    //            else
    //            {
    //                return false;
    //            }

    //        }
    //        else
    //        {
    //            //RefreshTokens = new List<RefreshTokenModel>();
    //            throw new Exception("old token is not exist");
    //        }
    //    }

    //    public RefreshTokenData? GetRefreshToken(string refreshToken)
    //    {
    //        if (RefreshTokens != null)
    //        {
    //            var exist = RefreshTokens.FirstOrDefault(x=>x.Token == refreshToken);
    //            return exist;
    //        }
    //        return null;
    //    }
    //}
}
