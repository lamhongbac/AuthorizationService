using System;
using System.Collections.Generic;

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

    public class ECheckListRequestDatasModel: RequestDatasModel 
    {
        public string UserGroup { get; set; }
    }

    public class FormQACheckListRequestDatasModel: RequestDatasModel
    {
        public List<int>? StoreIDs { get; set; }
    }

    public class OutletRequestDatasModel : RequestDatasModel
    {
        public string? CompanyCode { get; set; }
        public int NationID { get; set; }
        public int CityID { get; set; }
        public int DistrictID { get; set; }
    }

    public class QAChecklistRequestDatasModel : RequestDatasModel
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? Result { get; set; }
        public QAManagerFilter? QAManagerFilter { get; set; }
        public RestaurantFilter? RestaurantFilter { get; set; }
        public QAOfficerFilter? QAOfficerFilter { get; set; }
    }

    public class QAManagerFilter
    {
        public QAManagerFilter()
        {
            QAOfficerIDs = new List<string>();
        }
        public List<string>? QAOfficerIDs { get; set; }
    }

    public class RestaurantFilter
    {
        public RestaurantFilter()
        {
            StoreIDs = new List<int>();
        }
        public List<int>? StoreIDs { get; set; }
    }

    public class QAOfficerFilter
    {
        public string? QAOfficerID { get; set; }
    }

    public class OutletGroupRequestDatasModel : RequestDatasModel
    {
        public string? CompanyCode { get; set; }
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

    public class BrandRequestModel : RequestModel
    {
        public BrandRequestModel()
        {
            BusinessCatID = -1;
        }
        public int CompanyID { get; set; }
        public string CompanyCode { get; set; }
        public int? BusinessCatID { get; set; }
    }

    public class BusinessCatgoryRequestModel: RequestModel
    {
        public BusinessCatgoryRequestModel()
        {
            
        }
    }
}
