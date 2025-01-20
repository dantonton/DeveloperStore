using AutoMapper;
using DeveloperStore.API.Models.Sales;
using DeveloperStore.Domain.Commands;
using DeveloperStore.Domain.Models;

namespace DeveloperStore.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SalePostRequest, CreateSaleCommand>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.BranchName))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<SalePutRequest, UpdateSaleCommand>()
                .ForMember(dest => dest.SaleId, opt => opt.MapFrom(src => src.SaleId))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.BranchName))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<ItemSalePostRequest, SaleItem>();
        }
    }
}
