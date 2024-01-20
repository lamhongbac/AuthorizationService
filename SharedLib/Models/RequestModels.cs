using AuthServices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthServices.Models
{
    public class RequestDatasModel
    {
        public string? SortProperty { get; set; }
        public string? SearchText { get; set; }
        public string? sortOrder { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
    public class RequestModel
    {
        public RequestModel()
        {
            ID = 0;
            Number = " ";
        }
        public int ID { get; set; }
        public string? GuidID { get; set; }
        public string? Number { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class UpdateDataRequestModel : RequestModel
    {
        public string? CompanyCode { get; set; }
        public string? UserID { get; set; }
        public object? DataUpdate { get; set; }
    }
    public class AppUserRequestDatasModel : RequestDatasModel
    {
        public int CompanyAppID { get; set; }
        public string? Department { get; set; }
        public int ManagerID { get; set; }
    }

    public class AppRoleRequestDatasModel : RequestDatasModel
    {
        public int CompanyAppID { get; set; }
    }
}
