using System;

namespace SharedLib.Models
{
    public class RequestModel
    {
        public RequestModel()
        {
            ID = 0;
            Number = " ";
        }
        public int ID { get; set; }
        public Guid GuidID { get; set; }
        public string Number { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class UpdateDataRequestModel: RequestModel
    {
        public string CompanyCode { get; set; }
        public string UserID { get; set; }
        public object? DataUpdate { get; set; }
    }
}
