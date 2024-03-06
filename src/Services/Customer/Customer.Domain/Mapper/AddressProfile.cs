using AutoMapper;
using Customer.Domain.DTOs;
using Customer.Domain.Entities;

namespace Customer.Domain.Mapper
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDetailDto>().ReverseMap();
            CreateMap<Address, AddressCreateDto>().ReverseMap();
        }
    }
}
