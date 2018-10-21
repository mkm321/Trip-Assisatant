using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreContract = Core.Contracts;
using DataContract = Data.Contract;

namespace TripAssistantSearchEngineApi
{
    public class AutoMapperInitializer : Profile
    {
        public AutoMapperInitializer()
        {
            CreateMap<CoreContract.Response, DataContract.Response>();
        }
    }
}
