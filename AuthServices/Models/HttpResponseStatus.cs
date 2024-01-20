using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLib.Models
{
    public enum EHttpStatusCode
    {

        Moved = 301,
        OK = 200,
        Redirect = 302,
        UnAuthorized=401,
        Forbidden=403
    }
}
