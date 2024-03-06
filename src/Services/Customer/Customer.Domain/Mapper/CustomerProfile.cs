using AutoMapper;
using Customer.Domain.DTOs;

namespace Customer.Domain.Mapper
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Entities.Customer, CustomerDetailDto>().ReverseMap();
            CreateMap<Entities.Customer, CustomerCreateDto>().ReverseMap().ForMember(x => x.Address, opt => opt.Ignore());
        }
    }
}
