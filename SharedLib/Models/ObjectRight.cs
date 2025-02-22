using System;
using System.Collections.Generic;
using System.Text;

namespace AuthServices.Models
{
    /// <summary>
    /// Mo ta quyền trên 1 object
    /// ObjectName cung cap cho giao dien kha nang , kg can query ID
    /// 22 Feb 25 bo xung: objectID (ban chat liên kết la object ID)
    /// can viet lai ham get Object right bo xung objectID
    /// </summary>
    public struct ObjectRight
    {
        
        public int ObjectID { get; set; }
        public string ObjectName { get; set; }
        public List<string> Rights { get; set; }

        public bool CanList { get; set; }

        //neu kg the can read ==> kg the co cac quyen phia sau nhu can Create/Update,..vv

        public bool CanRead { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }

    }

    /// <summary>
    /// BObjectRight= Binary object right
    /// lop nay su dung thuat toan binary de quan ly quyen
    /// </summary>
    public class BObjectRight
    {

        public int ObjectID { get; set; }
        public string ObjectName { get; set; }
        public int Rights { get; set; }
    }


    /// <summary>
    /// enum mo ta cac quyen tren 1 object
    /// su dung 2^n de luu gia tri
    /// cung co the luu gia tri n= 1,2,3, 
    /// nhung khi luu vao CSDL can convert 2^n
    /// </summary>
    public enum EObjectRight { 
        List=2, //2^1
        Read=4, //2
        Update=8,//3
        Create=16,//4
        Delete=32,//5
    }
}
