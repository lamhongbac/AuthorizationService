using MSASharedLib.DataTypes;

namespace AuthServices.Models
{
    public class BaseCreateRequestModel : RequestModel
    {
        public object? Data { get; set; }
    }
}
