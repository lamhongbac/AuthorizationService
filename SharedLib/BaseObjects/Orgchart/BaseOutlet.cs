using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLib.BaseObjects.Orgchart
{
    public class BaseOutlet
    {
        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? BranchId { get; set; }
        public int BrandId { get; set; }
        public int? RegionId { get; set; }
        public int? CityId { get; set; }
        public int? DistrictId { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Tel { get; set; }
        public string? Website { get; set; }
        public string? Tags { get; set; }
        public decimal? Nps { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? OpenTime { get; set; }
        public string? CloseTime { get; set; }
        public bool? Parking { get; set; }
        public string? Picture { get; set; }
        public string? MerchantId { get; set; }
        /// <summary>
        /// Tầng ma outlet o trong Mall
        /// </summary>
        public int? FloorId { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsActive { get; set; }
        public string? Images { get; set; }
        public string? About { get; set; }
        public string? Icon { get; set; }
        public int? Priority { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public bool IsFranchise { get; set; }
        public string? EnDescription { get; set; }
        public string? EnAddress { get; set; }
        public string? EnName { get; set; }
    }
}
