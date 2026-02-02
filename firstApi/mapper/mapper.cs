using AutoMapper;
using firstApi.Dto;
using firstApi.Models;

namespace firstApi.mapper
{
    public class mapper : Profile
    {
        public mapper()
        {
            CreateMap<Vila, VilaDto>().ReverseMap();
          
        }
    }
}
