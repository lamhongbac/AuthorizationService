using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLib.BaseObjects.Orgchart
{
    public class BaseOrgchartCompany
    {
        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string PhoneNo { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Fax { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; } = null!;
        /// <summary>
        /// 0: Unlimited, &lt;&gt; 0 la gioi han
        /// </summary>
        public int? LicenseQuantity { get; set; }
        public string? Website { get; set; }
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }
        public string? Youtube { get; set; }
        public double? Longtitude { get; set; }
        public double? Latitude { get; set; }
        public bool IsDeleted { get; set; }
        public string? Slogan { get; set; }
        public int? CityId { get; set; }
        public int? DistrictId { get; set; }
        public int? ContentId { get; set; }
    }
}
