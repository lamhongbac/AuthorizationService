using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AuthServices.Models
{
    public class DepartmentDataSource
    {
        public DepartmentDataSource(string jsonfilePath)
        {
            DepartmentDatas = BuildDataSourceFromJson(jsonfilePath).Result;
        }
        private async Task<DepartmentDatas> BuildDataSourceFromJson(string fileName)
        {
            using FileStream openStream = File.OpenRead(fileName);
            var departmentDatas =
                await JsonSerializer.DeserializeAsync<List<DepartmentData>>(openStream);
            DepartmentDatas data = new DepartmentDatas(departmentDatas.ToList());
            
            return data;
        }

        public DepartmentDatas DepartmentDatas { get; set; }
    }

    public class DepartmentDatas
    {
        public DepartmentDatas(List<DepartmentData> departmentDatas)
        {
            DepartmentDataList = departmentDatas;
        }
        public List<DepartmentData>? DepartmentDataList { get; set; }
    }

    public class DepartmentData
    {
        public string? Value { get; set; }
        public string? Name { get; set; }
    }
}
