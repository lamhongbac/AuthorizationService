using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Linq;

namespace SharedLib.Utils
{
    public interface ILocationDataSource
    {
        List<WardData> WardDataSource { get; }
        List<DistrictData> DistrictDataSource { get; }
        List<ProvinceData> ProvinceDataSource { get; }
        int SelectedDistrictID { get; set; }
        int SelectedProvinceID { get; set; }
        int SelectedWardID { get; set; }

    }
    public class LocationDataSource : ILocationDataSource
    {
        public LocationDataSource(string jsonfilePath)
        {
            //string jsonfilePath = appFolder +"//"+ fileName;
            NationDataSource = BuildDataSourceFromJson(jsonfilePath).Result;
        }

        private async Task<NationData> BuildDataSourceFromJson(string fileName)
        {
            using FileStream openStream = File.OpenRead(fileName);
            var provinces =
                await JsonSerializer.DeserializeAsync<List<ProvinceData>>(openStream);
            NationData data = new NationData(provinces.ToList());
            if (data.Provinces.Count > 0)
            {
                SelectedProvinceID = data.Provinces[0].ID;
                if (data.Provinces[0].Districts.Count > 0)
                    SelectedDistrictID = data.Provinces[0].Districts[0].ID;
                else
                    SelectedDistrictID = -1;
            }
            else
            {
                SelectedProvinceID = -1;
                SelectedDistrictID = -1;
            }


            return data;
        }

        NationData NationDataSource;
        public int SelectedProvinceID { get; set; }
        public int SelectedDistrictID { get; set; }
        public int SelectedWardID { get; set; }
        public List<ProvinceData> ProvinceDataSource
        {
            get
            {
                if (NationDataSource.Provinces != null)
                    return NationDataSource.Provinces;
                else
                    return null;
            }
        }

        public List<DistrictData> DistrictDataSource
        {
            get
            {
                //provice data kg co gia tri 0,
                //co 1 so truong hop giao dien truyen 0 se co loi
                if (SelectedProvinceID > 0)
                {

                    ProvinceData provinceData = ProvinceDataSource.
                                        Where(x => x.ID == SelectedProvinceID).FirstOrDefault();
                    if (provinceData != null)
                        return provinceData.Districts;
                    else
                        return null;

                    //return ProvinceDataSource.
                    //Where(x => x.ID == SelectedProvinceID)
                    //.Select(x => x.Districts).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
        }
        public List<WardData> WardDataSource
        {
            get
            {
                if (SelectedDistrictID > 0)
                {
                    return DistrictDataSource.
                    Where(x => x.ID == SelectedDistrictID).
                    FirstOrDefault().Wards;
                }
                else
                {
                    return null;
                }
            }
        }
    }
    public class ItemData
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
    /// <summary>
    /// 1 nation co n Province / city
    /// </summary>
    public class NationData : ItemData
    {
        public NationData()
        {

        }
        public NationData(List<ProvinceData> provinces)
        {
            Provinces = provinces;
        }
        public List<ProvinceData> Provinces { get; set; }
    }

    /// <summary>
    /// 1 province data co n district
    /// </summary>
    public class ProvinceData : ItemData
    {
        public List<DistrictData> Districts { get; set; }
    }
    /// <summary>
    /// 1 district co n ward
    /// </summary>
    public class DistrictData : ItemData
    {
        public List<WardData> Wards { get; set; }
    }

    public class WardData : ItemData
    {

    }
}
