using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLib.Authentication
{
    public class IMappingHelper<S, T>
    {
        public S Map(T mapData)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<S, T>().ReverseMap());
            var mapper = config.CreateMapper();
            S data = mapper.Map<S>(mapData);
            return data;
        }

        public List<S> Map(List<T> mapData)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<S, T>().ReverseMap());
            var mapper = config.CreateMapper();
            List<S> data = mapper.Map<List<S>>(mapData);
            return data;
        }
    }
}
