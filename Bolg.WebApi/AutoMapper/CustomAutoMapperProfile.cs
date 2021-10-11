using AutoMapper;
using Blog.Model;
using Blog.Model.DTO;

namespace Blog.WebApi.AutoMapper
{
    public class CustomAutoMapperProfile : Profile
    {
        public CustomAutoMapperProfile()
        {
            base.CreateMap<WriteInfo, WriteDTO>();
        }
    }
}
