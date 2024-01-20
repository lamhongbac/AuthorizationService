using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedLib.Utils
{
    public class PageDataService<T>
    {
        public List<T> GetData(List<T> list, int pageIndex, int pageSize)
        {
            var item = list.Skip((pageIndex - 1)*pageSize).Take(pageSize).ToList();
            return item;
        }
    }
}
