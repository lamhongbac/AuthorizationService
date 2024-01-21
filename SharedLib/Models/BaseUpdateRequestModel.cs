using MSASharedLib.DataTypes;

namespace AuthServices.Models
{
    public class BaseUpdateRequestModel : RequestModel
    {
        public object? Data { get; set; }
    }
}
