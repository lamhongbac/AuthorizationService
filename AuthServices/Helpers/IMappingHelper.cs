using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthServices.Helpers
{
    public class IMappingHelper<T, S>
    {
        public S Map(T mapData)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T, S>());
            var mapper = config.CreateMapper();
            S data = mapper.Map<S>(mapData);
            return data;
        }
    }
}
