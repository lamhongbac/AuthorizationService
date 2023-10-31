using System;

namespace SharedLib
{
    public class CommonDataTypes
    {

    }
    public enum AppUserType
    {
        UserName,
        Email,
        MobileNo
    }
    public enum JwtStatus
    {
        InvalidToken,
        TokenIsNotExpired,
        TokenIsNotExist,
        TokenIsUsed,
        IsRevoked,
        AccessTokenIdIsNotMatch,
        Success,
        BadRequest
    }
}
