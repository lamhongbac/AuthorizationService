using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLib.Models
{
    public class RequestDatasModel
    {
        public string? SortProperty { get; set; }
        public string? SearchText { get; set; }
        public string? sortOrder { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class OutletRequestDatasModel: RequestDatasModel
    {
        public string? CompanyCode { get; set; }
        public int NationID { get; set; }
        public int CityID { get; set; }
        public int DistrictID { get; set; }
    }

    public class OutletGroupRequestDatasModel: RequestDatasModel
    {
        public string? CompanyCode { get; set; }
    }

    public class AppUserRequestDatasModel : RequestDatasModel
    {
        public int CompanyAppID { get; set; }
    }

    public class AppRoleRequestDatasModel : RequestDatasModel
    {
        public int CompanyAppID { get; set; }
    }
}
