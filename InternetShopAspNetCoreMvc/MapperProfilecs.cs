using AutoMapper;
using InternetShopAspNetCoreMvc.Models;
using InternetShopAspNetCoreMvc.ViewModels;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateProductViewModel, Product>()
            .ForMember(dest => dest.Image, opt => opt.Ignore()) 
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        CreateMap<EditProductViewModel, Product>()
            .ForMember(dest => dest.Image, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        CreateMap<Product, EditProductViewModel>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));
    }
}
